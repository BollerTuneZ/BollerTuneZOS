using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Communication.Infrastructure.MessageProcessor;
using Data.Settings;
using Data.Steering;
using Data.Steering.Enums;
using Infrastructure.Communication;
using Infrastructure.Data.Settings;
using Infrastructure.Util;
using log4net;
namespace BollerTuneZCore.Processors
{
    /// <summary>
    /// Processor fürs Lenken
    /// Jonas Ahlf 19.06.2015 22:34:10
    /// </summary>
    public class SteeringProcessor : ISteeringProcessor
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (SteeringProcessor));
        private Thread _steeringThread;
        private readonly ISettingsRepository _settingsRepository;
        private IBTZSocket _socket;
        private object _lockObject = new object();
        private object _serialLock = new object();
        private volatile int _remotePosition;
        private int _currentSteeringPosition;
        private int _currentMotorPosition;
        private SteeringSettings _settings;
        private volatile bool Run = false;
        private volatile bool Enabled = false;

        public SteeringProcessor(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _settings = _settingsRepository.RetriveSteeringSettings();
        }

        public void Initialize(IBTZSocket socket)
        {
            _socket = socket;
            _steeringThread = new Thread(SteeringService);
        }

        public void Start()
        {
            SLog.Debug("Start SteeringService...");
            Run = true;
            _steeringThread.Start();
            SLog.Debug("SteeringService -> started");

        }

        public void Stop()
        {
            SLog.Debug("Stopping SteeringService...");
            Run = false;
            _steeringThread.Abort();
            SLog.Debug("SteeringService -> stopped");
        }

        public void SetPosition(int position)
        {
            lock (_lockObject)
            {
                SLog.DebugFormat("Set RemotePosition from {0} to {1}",_remotePosition,position);
                _remotePosition = position;
            }
        }

        void SteeringService()
        {
            while (Run)
            {
                //Position vom Arduino holen
                RefreshControllerPositions();

                var currentRemotePosition = 0;
                lock (_lockObject)
                {
                    currentRemotePosition = _remotePosition;
                }
                //Bewegung erstellen
                SteeringMovement movement = CalculateSteeringMovement(currentRemotePosition);

                if (movement.Direction == SteeringDirection.Non)
                {
                    //Es ist nicht nötig eine Bewegung durchzuführen
                    continue;
                }
                var serializedArduioMessages = CreateMessages(movement);

                foreach (var serializedArduioMessage in serializedArduioMessages)
                {
                    //TODO Sollte der Arduino davon wissen und vielleicht werte runterfahren ?
                    if (Enabled)//Nur ausführen wenn die Lenkung aktiviert ist (Nur das controlling abfragen laufen weiter)
                    {
                        lock (_serialLock)
                        {
                            _socket.SendData(serializedArduioMessage);
                        }
                    }
                }

            }
        }

        #region Controlling

        /// <summary>
        /// Erstellt die Serialisierten Messages die an den Arduino gehen
        /// </summary>
        /// <param name="movement"></param>
        List<byte[]> CreateMessages(SteeringMovement movement)
        {
            var messages = new List<byte[]>();
            var directionMessage = new List<byte>();
            var speedMessage = new List<byte>();
            //DirectionMessage
            directionMessage.Add(SerialConstants.COMMAND_DIRECTION_STEERING); //DirectionCommand
            switch (movement.Direction)
            {
                case SteeringDirection.Non:
                    directionMessage.Add(0x00);
                    break;
                case SteeringDirection.Left:
                    directionMessage.Add(SerialConstants.DIRECTION_LEFT);
                    break;
                case SteeringDirection.Right:
                    directionMessage.Add(SerialConstants.DIRECTION_RIGHT);
                    break;
            }
            messages.Add(directionMessage.ToArray());
            //SpeedMessage
            speedMessage.Add(SerialConstants.COMMAND_POWER);
            speedMessage.Add(Convert.ToByte(movement.Speed));
            messages.Add(speedMessage.ToArray());
            return messages;
        }
        #endregion

        #region Kalkulation der Geschwindigkeit und richtung

        private SteeringMovement CalculateSteeringMovement(int position)
        {
            //Remoteposition auf die echt umrechnen
            var mappedRemote = MathHelper.Map(position, _settings.RemoteMin, _settings.RemoteMax, _settings.SteeringMin,
                _settings.SteeringMax);
            SLog.DebugFormat("Map RemotePosition {0} to RealPosition {1}",position,mappedRemote);

            //Den unterschied zwischen echter und remote ermitteln
            var drift = MathHelper.IsToleratedReturnPercentage(mappedRemote, _currentMotorPosition,_settings.SteeringPositionDiffTolerance);
            SLog.DebugFormat("Calculated drift {0}, tolerated({1})", drift.Item2, drift.Item1);

            //prüfen ob es sich noch in der Toleranzgrenze befindet
            if (drift.Item1)
            {
                SLog.Debug("Differenz is tolerated");
                return new SteeringMovement(SteeringDirection.Non, 0);
            }

            var _direction = SteeringDirection.Non;
            //Richtung bestimmen > 100 = rechts; < 100 = links 
            if (drift.Item2 > 100)
            {
                _direction = SteeringDirection.Right; 
            }
            else
            {
                _direction = SteeringDirection.Left; 
            }
            int diff = (int)((drift.Item2 - 100)*(_currentMotorPosition/100));
            SLog.DebugFormat("Direction is set to: {0}, and the differenz is {1}", _direction,diff);

            int speed = MathHelper.Map(diff, _settings.RemoteMin, _settings.RemoteMax, _settings.SteeringSpeedMin,
                _settings.SteeringSpeedMax);

            var movement = new SteeringMovement(_direction,speed);
            SLog.DebugFormat("Movement created with Direction: {0} and Speed {1}",_direction,speed);
            return movement;
        }
        #endregion

        #region ControllerPosition update
        void RefreshControllerPositions()
        {
            //Message pattern {EN}:<SteeringPosition>/<MotorPosition>
            string received = String.Empty;
            try
            {
                received = Encoding.Default.GetString(_socket.ReceiveData());
                SLog.DebugFormat("Received Data ({0})",received);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not get Data from SteeringController {0}", e);
                return;
            }
            if (String.IsNullOrWhiteSpace(received))
            {
                SLog.Error("response from SteeringController is Invalid");
                return;
            }
            const string expectedTag = "{EN}:";
            if (!received.ToLower().Contains(expectedTag.ToLower()))
            {
                SLog.WarnFormat("Receiveddata does not contain what we thought :( {0}, expected {1}", received, expectedTag);
                return;
            }
            const char seperator = '/';
            string cleanedString = received.Replace(expectedTag, "");
            var splitValues = cleanedString.Split(seperator);
            int steeringPosition;
            if (!int.TryParse(splitValues[0], out steeringPosition))
            {
                SLog.ErrorFormat("Not able to Parse {0} into int steeringPosition", splitValues[0]);
                return;
            }
            int motorPosition;
            if (!int.TryParse(splitValues[1], out motorPosition))
            {
                SLog.ErrorFormat("Not able to Parse {0} into int motorPosition", splitValues[1]);
                return;
            }
            _currentMotorPosition = motorPosition;
            _currentSteeringPosition = steeringPosition;
        }

        byte[] CreateEncoderSetMessage(EncoderType type, int value)
        {
            var message = new List<byte> {SerialConstants.COMMAND_SET_ENCODER};
            switch (type)
            {
                case EncoderType.Steering:
                    message.Add(SerialConstants.ENCODER_STEERING);
                    break;
                case EncoderType.Motor:
                    message.Add(SerialConstants.ENCODER_MOTOR);
                    break;
            }
            var array = Encoding.ASCII.GetBytes(value.ToString());
            message.AddRange(array);
            return message.ToArray();
        }

        #endregion

        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void SetEncoderPosition(EncoderType encoderType, int position)
        {
            var message = CreateEncoderSetMessage(encoderType, position);

            lock (_serialLock)
            {
                _socket.SendData(message);
            }
        }
    }
}
