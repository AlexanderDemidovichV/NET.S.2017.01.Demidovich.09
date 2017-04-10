using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Task1;
using ILogger = Task1.ILogger;

namespace NLoggerAdapter
{
    public class LoggerAdapter: ILogger
    {
        private readonly Logger logger;

        public LoggerAdapter(Logger logger)
        {
            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException();

            this.logger = logger;
        }

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Debug:
                    logger.Debug(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Error:
                    logger.Error(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Information:
                    logger.Info(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Fatal:
                    logger.Fatal(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Warning:
                    logger.Warn(entry.Exception, entry.Message);
                    break;
            }

        }
    }
}
