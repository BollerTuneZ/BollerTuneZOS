using System;
using System.ComponentModel;
using System.Threading;
using System.Timers;
using Communication.Infrastructure;
using Communication.Infrastructure.MessageProcessor;
using Data.Settings;
using Infrastructure;
using Infrastructure.Communication;
using Infrastructure.Data.Settings;
using log4net;

namespace Communication.MessageProcessor
{
	public class EngineProcessor : IEngineProcessor
	{
	    private ISettingsRepository _settingsRepository;
	    private static IBTZSocket _socket;
	    private static readonly ILog SLog = LogManager.GetLogger(typeof (EngineProcessor));
	    private Thread _driveThread;
        private object _speedLock = new object();
        private object _directionLock = new object();
	    private volatile bool _run = false;
	    private volatile bool _enabled = false;
	    private volatile int _speed;
	    private volatile byte _direction;
	    private EngineSettings _settings;


	    public EngineProcessor(ISettingsRepository settingsRepository)
	    {
	        _settingsRepository = settingsRepository;
	        _settings = _settingsRepository.RetriveEngineSettings();
	    }

	    /*Constante*/
	    private const byte DirectionForwards = 0x46;
        private const byte DirectionBackwards = 0x42;
        private const byte DirectionNon = 0x00;


	    public void Initialize()
	    {
	       // _socket = socket;
	    }

	    public void Start()
	    {
            SLog.Debug("Starting -> DriveService");
            _driveThread = new Thread(DriveService);
	        _run = true;
            _driveThread.Start();
            SLog.Debug("Started -> DriveService");
        }

	    public void Stop()
	    {
            SLog.Debug("Stopping -> DriveService");
	        _run = false;
            _driveThread.Abort();
            SLog.Debug("Stopped -> DriveService");
	    }

	    public void SetSpeed(int speed)
	    {
	        lock (_speedLock)
	        {
                SLog.DebugFormat("Set Speed from {0} to {1}",_speed,speed);
	            _speed = speed;
	        }
	    }

	    public void SetDirection(EngineDriveDirection direction)
	    {
	        lock (_directionLock)
	        {
                switch (direction)
                {
                    case EngineDriveDirection.Non:
                        _direction = DirectionNon;
                        break;
                    case EngineDriveDirection.Forwards:
                        _direction = DirectionForwards;
                        break;
                    case EngineDriveDirection.Backwards:
                        _direction = DirectionBackwards;
                        break;
                } 
	        }
            SLog.DebugFormat("Direction set to {0}", _direction);
	    }

	    public void SetEnabled(bool enabled)
	    {
            SLog.DebugFormat("Set Enabled from {0} to {1}",_enabled,enabled);
	        _enabled = enabled;
	    }

	    private void DriveService()
	    {
            while (_run)
            {
                var messages = CreateMessages();
                if (!_enabled) continue;
                _socket.SendData(messages.Item1);
                _socket.SendData(messages.Item2);
            }
	    }

	    private Tuple<byte[],byte[]> CreateMessages()
	    {
	        byte[] speedMessage;
            byte[] directionMessage;
	        lock (_speedLock)
	        {
                speedMessage = new byte[]
	            {
                    SerialConstants.COMMAND_DRIVE_POWER,
                    Convert.ToByte(CalculateSpeed())
	            };
	        }
	        lock (_directionLock)
	        {
                directionMessage = new byte[2]
	            {
                    SerialConstants.COMMAND_DIRECTION_DRIVE,
                    _direction
	            };
	        }

            return new Tuple<byte[], byte[]>(speedMessage, directionMessage);
	    }

	    int CalculateSpeed()
	    {
	        if (_speed < _settings.EngineSpeedStartMin)
	        {
	            return 0;
	        }else if (_speed > _settings.EngineSpeedMax)
	        {
	            return _settings.EngineSpeedMax;
	        }
	        else
	        {
	            return _speed;
	        }
	    }
	}
}

