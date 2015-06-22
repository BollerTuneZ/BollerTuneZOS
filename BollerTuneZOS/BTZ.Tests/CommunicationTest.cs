using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BTZ.Tests.Infrastructure;
using Communication.Serial;
using Infrastructure.Communication;
using log4net;

namespace BTZ.Tests
{
    /// <summary>
    /// Test der Kommunikation zwischen Server und Arduinos
    /// Jonas Ahlf 21.06.2015 11:35:16
    /// </summary>
    public class CommunicationTest : ITest
    {
        public event EventHandler OnTestFinish;
        private static readonly ILog SLog = LogManager.GetLogger(typeof (CommunicationTest)); 
        private ISerialDeviceHelper _deviceHelper;
        private IBTZSocket _steeringSocket;
        private IBTZSocket _engineSocket;
        private object DeviceLock = new object();
        private volatile bool _devicesFound = false;

        public CommunicationTest()
        {
            _deviceHelper = new SerialDeviceHelper();
            _deviceHelper.OnDeviceFound += OnDeviceFound;
        }

        private void OnDeviceFound(object sender, EventArgs eventArgs)
        {
            var device = (EventArgsDeviceFound) eventArgs;
            SLog.DebugFormat("Device found {0}",device.Device.ComPort);

            if (device.Device.Type == DeviceType.Drive)
            {
                SLog.DebugFormat("DriveSocket found {0}", device.Device.ComPort);
                _engineSocket = device.Device;
            }else if (device.Device.Type == DeviceType.Steering)
            {
                SLog.DebugFormat("SteeringSocket found {0}", device.Device.ComPort);
                _steeringSocket = device.Device;
            }
            if (_engineSocket != null && _steeringSocket != null)
            {
                _devicesFound = true;
            }
        }

        public void Start()
        {
            SLog.Debug("Start searching for Serial Sockets");
            _deviceHelper.StartDiscover();
            DateTime lastTime = DateTime.Now;
            while (!_devicesFound)
            {
                Thread.Sleep(100);
                if ((DateTime.Now.Subtract(lastTime)).TotalSeconds >= 1)
                {
                    SLog.Debug("Searching for devices...");
                    lastTime = DateTime.Now;
                }
            }
            _deviceHelper.StopDiscover();
            SLog.Debug("CommonicationTest begin");
            Thread streamThread = new Thread(StreamReadService);
            //streamThread.Start();
            //Steering Tests
            SLog.Debug("Steering Tests");
            lock (DeviceLock)
            {
                SLog.Debug("Send Steering Direction LEFT");
                _steeringSocket.SendData(new byte[]{SerialConstants.COMMAND_DIRECTION_STEERING,SerialConstants.DIRECTION_LEFT});
                SLog.Debug("Send Steering Direction RIGHT");
                _steeringSocket.SendData(new byte[] { SerialConstants.COMMAND_DIRECTION_STEERING, SerialConstants.DIRECTION_RIGHT });
                for (int i = 0; i <= 255; i++)
                {
                    SLog.DebugFormat("Send Steering power {0}",i);
                    _steeringSocket.SendData(new byte[] { SerialConstants.COMMAND_POWER, Convert.ToByte(i) });
                    Thread.Sleep(200);
                    var readBytes = _steeringSocket.ReceiveData();
                    if (readBytes.Any())
                    {
                        var value = Encoding.Default.GetString(readBytes);
                        SLog.InfoFormat("Got Value from Steering {0}",value);
                    }
                }
            }
            //EngineTests
            lock (DeviceLock)
            {
                SLog.Debug("Test Power");
                for (int i = 0; i <= 255; i++)
                {
                    SLog.DebugFormat("Send Engine power {0}",i);
                    _engineSocket.SendData(new []{SerialConstants.COMMAND_DRIVE_POWER,Convert.ToByte(i)});
                    Thread.Sleep(200);
                }
                SLog.Debug("Send Engine Direction FORWARD");
                _engineSocket.SendData(new byte[]{ SerialConstants.COMMAND_DIRECTION_DRIVE, 0x46 });
                SLog.Debug("Send Engine Direction BACKWARD");
                _engineSocket.SendData(new byte[] { SerialConstants.COMMAND_DIRECTION_DRIVE, 0x42 });
            }
            SLog.Debug("Tests finished");
            streamThread.Abort();
        }

        void StreamReadService()
        {
            while (true)
            {
                string receivedSteering = "";
                string receivedEngine = "";
                try
                {
                    lock (DeviceLock)
                    {
                        receivedSteering = Encoding.Default.GetString(_steeringSocket.ReceiveData());
                        receivedEngine = Encoding.Default.GetString(_engineSocket.ReceiveData());
                    }
                }
                catch (Exception e)
                {
                    SLog.ErrorFormat("Could not read from sockets {0}",e);
                    continue;
                }
                if (!String.IsNullOrWhiteSpace(receivedEngine))
                {
                    SLog.DebugFormat("Engine: {0}",receivedEngine);
                }
                if (!String.IsNullOrWhiteSpace(receivedSteering))
                {
                    SLog.DebugFormat("Steering: {0}", receivedSteering);
                }
            }

        }

    }
}
