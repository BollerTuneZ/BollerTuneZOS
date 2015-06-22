using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Communication;
using log4net;

namespace Communication.Serial
{
    /// <summary>
    /// Jonas Ahlf 19.06.2015 17:59:41
    /// </summary>
    public class SerialDeviceHelper : ISerialDeviceHelper
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof(SerialDeviceHelper));
        private Thread _discoverThread;
        public event EventHandler OnDeviceFound;

        private IList<string> _blackList;

        public SerialDeviceHelper()
        {
            _discoverThread = new Thread(Discover);
        }

        public void StartDiscover()
        {
            SLog.Debug("Start Discover Serial Devices");
            _discoverThread.Start();
        }

        void Discover()
        {
            _blackList = new List<string>();
            while (true)
            {
                string[] avaiblePorts = SerialPort.GetPortNames();

                foreach (var avaiblePort in avaiblePorts)
                {
                    var tempSerialPort = new SerialPort(avaiblePort, SerialConstants.BOUD_RATE);
                    var detectedDevice = DetectDevice(tempSerialPort);
                    if (detectedDevice == null)
                    {
                        continue;
                    }
                    OnDeviceFound(this,new EventArgsDeviceFound{Device = detectedDevice});
                }
            }
        }

        SerialDevice DetectDevice(SerialPort port)
        {
            var detectMessage = new byte[2] {SerialConstants.START_BYTE,SerialConstants.COMMAND_PRINT_IDENTITY};
            try
            {
                if (_blackList.Any(p => p.Equals(port.PortName)))
                {
                    SLog.DebugFormat("Port already known {0}",port.PortName);
                    return null;
                }
                SLog.DebugFormat("Open COM Port {0}",port.PortName);
                port.Open();
                port.Write(detectMessage,0,detectMessage.Length);
                Thread.Sleep(1000);
                int count = port.BytesToRead;
                string returnMessage = "";
                int intReturnASCII = 0;
                while (count > 0)
                {
                    intReturnASCII = port.ReadByte();
                    returnMessage = returnMessage + Convert.ToChar(intReturnASCII);
                    count--;
                }
                port.Close();
                if (String.IsNullOrWhiteSpace(returnMessage))
                {
                    SLog.ErrorFormat("Bad answer from {0}",port.PortName);
                    return null;
                }
                SerialDevice device = new SerialDevice
                {
                    SerialPort = port,
                    ComPort = port.PortName
                };
                if (returnMessage.Contains("STEERING"))
                {
                    device.Type = DeviceType.Steering;
                    _blackList.Add(device.ComPort);
                }
                else if (returnMessage.Contains("DRIVE"))
                {
                    _blackList.Add(device.ComPort);
                    device.Type = DeviceType.Drive;
                }
                return device;
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not reach {0}, {1}",port.PortName,e);
                return null;
            }
        }

        public void StopDiscover()
        {
            SLog.Debug("Stop Discover Serial Devices");
            _discoverThread.Abort();
        }
    }
}
