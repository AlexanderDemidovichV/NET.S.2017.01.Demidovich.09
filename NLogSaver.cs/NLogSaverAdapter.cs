using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task1
{
    public class NLogSaverAdapter : ILogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Debug: logger.Debug(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Error: logger.Error(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Information: logger.Info(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Fatal: logger.Fatal(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Warning: logger.Warn(entry.Exception, entry.Message);
                    break;
            }
            
        }

        public void Flush() => LogManager.Flush();
    }
}
