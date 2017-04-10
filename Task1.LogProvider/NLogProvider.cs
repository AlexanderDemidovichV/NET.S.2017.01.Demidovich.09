using System;
using NLog;
using Task1.LogAdapter;
using ILogger = Task1.LogAdapter.ILogger;

namespace Task1.LogProvider
{
    /// <summary>
    /// Provides a factory to create an instance of logger 
    /// adapted to <see cref="ILogger"/> interface.
    /// </summary>
    public static class NLogProvider
    {
        /// <summary>
        /// Factory method. Returns instance of logger for
        /// specified classname
        /// </summary>
        /// <param name="className">Specified classname</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws if 
        /// <paramref name="className"/> is null</exception>
        public static ILogger GetLogger(string className)
        {
            if (ReferenceEquals(className, null))
                throw new ArgumentNullException($"{nameof(className)} is null.");

            return new NLoggerAdapter(LogManager.GetLogger(className));
        }

        /// <summary>
        /// Flushes logs.
        /// </summary>
        public static void Flush() => LogManager.Flush();
    }
}
