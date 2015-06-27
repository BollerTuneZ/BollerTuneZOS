using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Btz.Debugging
{
    /// <summary>
    /// Log Class for connected Arduinos in Debug Build
    /// Jonas Ahlf 27.06.2015 13:05:21
    /// </summary>
    public static class LogBtzArduino
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (LogBtzArduino));


        public static Tuple<bool, string> Log(byte[] data)
        {
            string strValue = "";
            try
            {
                strValue = Encoding.Default.GetString(data);
            }
            catch (Exception e)
            {
                SLog.WarnFormat("Could not convert data to string {0}",e);
                return new Tuple<bool, string>(false, null);
            }
            var arduinoMessage = new ArduinoLogMessage();
            arduinoMessage.ParseMessage(strValue);
            if (!arduinoMessage.IsLegal)
            {
                SLog.WarnFormat("Arduino message is not Legal");
                return new Tuple<bool, string>(false, strValue);
            }
            PrintArduinoMessage(arduinoMessage);
            return new Tuple<bool, string>(true, null);
        }

        static void PrintArduinoMessage(ArduinoLogMessage message)
        {
            //Pattern Log LOG_%IDENTITY% Class[id]{LEVEL} Message
            SLog.DebugFormat("{0} {1}[{2}] {3}",new object[]{message.Device,message.ArduinoClass,message.LogLevel,message.Message});
        }

    }

    internal class ArduinoLogMessage
    {
        private const string DeviceNameSteering = "STEERING";
        private const string DeviceNameEngine = "ENGINE";
        public bool IsLegal { get; private set; }
        public ArduinoLogLevel LogLevel { get; private set; }
        public ArduinoDevice Device { get; private set; }
        public string ArduinoClass { get; private set; }
        public int ArduinoLogId { get; private set; }
        public string Message { get; private set; }
        //Pattern Log LOG_%IDENTITY% Class[id]{LEVEL} Message
        public void ParseMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                IsLegal = false;
                return;
            }
            if (!message.Contains("LOG_"))
            {
                IsLegal = false;
                return;
            }
            message = message.Replace("LOG_", "");
            int firstIndexWhiteSpace = message.IndexOf(' ');
            var deviceString = message.Substring(0, firstIndexWhiteSpace);
            if (deviceString.Equals(DeviceNameSteering))
            {
                Device = ArduinoDevice.Steering;
            }
            else if (deviceString.Equals(DeviceNameEngine))
            {
                Device = ArduinoDevice.Engine;
            }
            else
            {
                Device = ArduinoDevice.Non;
                IsLegal = false;
                return;
            }
            message = message.Replace(deviceString + " ", "");
            int length = message.Length;
            firstIndexWhiteSpace = message.IndexOf(' ');
            var logMessage = message.Substring(firstIndexWhiteSpace +1);
            if (String.IsNullOrWhiteSpace(logMessage))
            {
                IsLegal = false;
                return;
            }
            Message = message;
            message = message.Replace(" " + logMessage,"");
            int indexOfStartId = message.IndexOf('[') + 1;
            int idLength = message.IndexOf(']') - indexOfStartId;
            var logIndexStr = message.Substring(indexOfStartId ,idLength);
            if (String.IsNullOrWhiteSpace(logIndexStr))
            {
                IsLegal = false;
                return;
            }
            int logIndex;
            if (!int.TryParse(logIndexStr,out logIndex))
            {
                IsLegal = false;
                return;
            }
            message = message.Replace(String.Format("[{0}]", logIndex), "");
            int indexOfStartLogLevel = message.IndexOf('{') + 1;
            int LogLevelLength = message.IndexOf('}') - indexOfStartLogLevel;
            var logLevelStr = message.Substring(indexOfStartLogLevel, LogLevelLength);
            if (String.IsNullOrWhiteSpace(logLevelStr))
            {
                IsLegal = false;
                return;
            }
            ArduinoLogLevel tempLogLevel;
            if (!Enum.TryParse(logLevelStr,true,out tempLogLevel))
            {
                IsLegal = false;
                return;
            }
            LogLevel = tempLogLevel;
            IsLegal = true;
        }

    }



    internal enum ArduinoDevice
    {
        Non,
        Engine,
        Steering
    }

    internal enum ArduinoLogLevel
    {
        Debug,
        Warn,
        Error,
        Info
    }
}
