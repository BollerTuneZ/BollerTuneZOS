using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Infrastructure.MessageProcessor;
using log4net;

namespace EngineModbusLib
{
    public class EngineProcessor : IEngineProcessor
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (EngineProcessor));


        public void Initialize()
        {
            SLog.Debug("Fake Initialize Engine processor");
        }

        public void Start()
        {
            SLog.Debug("Fake Start Engine processor");
        }

        public void Stop()
        {
            SLog.Debug("Fake Stop Engine processor");
        }

        public void SetSpeed(int speed)
        {
            SLog.DebugFormat("Set Speed to {0}",speed);
        }

        public void SetDirection(EngineDriveDirection direction)
        {
            SLog.DebugFormat("Set Direction to {0}",direction);
        }

        public void SetEnabled(bool enabled)
        {
            SLog.DebugFormat("Set Enabled to {0}", enabled);
        }
    }
}
