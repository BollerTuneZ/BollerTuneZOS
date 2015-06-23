using System;
using System.Collections.Generic;
using System.IO.Ports;
using Infrastructure.Communication;
using log4net;

namespace Communication.Serial
{
    /// <summary>
    /// Bildet ein Serielles Gerät ab 
    /// Über diese klasse kann mit dem Gerät Kommuniziert werden
    /// Jonas Ahlf 19.06.2015 17:22:31
    /// </summary>
    public class SerialDevice : IBTZSocket
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (SerialDevice));

        public string ComPort { get; set; }

        public SerialPort SerialPort { get; set; }

        public DeviceType Type { get; set; }

        public void SendData(byte[] payload)
        {
            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.Open();
                }
                SLog.DebugFormat("Send Data to {0}:{1} : {2}",Type,SerialPort.PortName,payload);
                SerialPort.Write(payload, 0, payload.Length);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not send Data {0} to Device {1}:{2}, {3}",
                    new object[]{payload,Type.ToString(),SerialPort.PortName,e});
            }
        }

        public byte[] ReceiveData()
        {
            int bytesToRead;
            var buffer = new List<byte>();
            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.Open();
                }
                bytesToRead = SerialPort.BytesToRead;
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not read from {0}:{1}, {2}",Type,SerialPort.PortName,e);
                return null;
            }
            SLog.DebugFormat("Socket {0}, bytesToRead {1}",SerialPort.PortName,bytesToRead);
            while (bytesToRead > 0)
            {
                buffer.Add(Convert.ToByte(SerialPort.ReadByte()));
                bytesToRead--;
            }
            return buffer.ToArray();
        }
    }

    public enum DeviceType
    {
        Non,
        Steering,
        Drive
    }
}
