using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace BTZ.Test.Infrastructure
{
    /// <summary>
    /// Jonas Ahlf 22.06.2015 22:44:57
    /// </summary>
    public static class LogInitializer
    {
        public static void Init(string name,Level logLevel)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = String.Format(@"Logs\{0}.txt",name);
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            ConsoleAppender consoleAppender = new ConsoleAppender
            {
                Layout = patternLayout,
            };
            consoleAppender.ActivateOptions();
            hierarchy.Root.AddAppender(consoleAppender);

            hierarchy.Root.Level = logLevel;
            hierarchy.Configured = true;
        }
    }
}
