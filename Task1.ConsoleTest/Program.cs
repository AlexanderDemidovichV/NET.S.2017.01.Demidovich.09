using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NLog;

namespace Task1.ConsoleTest
{
    class Program
    {

        private static readonly ILogger logger = new NLogSaverAdapter();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            logger.Log(new LogEntry(LoggingEventType.Warning, "just checking"));
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
                logger.Log(new LogEntry(LoggingEventType.Fatal, "Unhandled exception: {0}", (Exception)e.ExceptionObject));;
                logger.Flush();
        }
}


}
