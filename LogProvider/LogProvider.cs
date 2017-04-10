using System;
using NLog;
using NLoggerAdapter;

namespace LogProvider
{
    public static class NLogProvider
    {
        public static Task1.ILogger GetLogger(string className)
        {
            if (ReferenceEquals(className, null))
                throw  new ArgumentNullException();

            return new LoggerAdapter(LogManager.GetLogger(className));
        }

        public static void Flush() => LogManager.Flush();
    }
}
