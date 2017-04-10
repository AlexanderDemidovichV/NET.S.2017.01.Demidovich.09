using System;
using NLog;
using Task1.LogAdapter;
using ILogger = Task1.LogAdapter.ILogger;

namespace Task1.LogProvider
{
    public static class NLogProvider
    {
        public static ILogger GetLogger(string className)
        {
            if (ReferenceEquals(className, null))
                throw new ArgumentNullException($"{nameof(className)} is null.");

            return new NLoggerAdapter(LogManager.GetLogger(className));
        }

        public static void Flush() => LogManager.Flush();
    }
}
