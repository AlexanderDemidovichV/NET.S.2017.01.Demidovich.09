using System;
using NLog;
using Task1.LogAdapter;
using Task1.LogProvider;
using ILogger = Task1.LogAdapter.ILogger;

namespace Task1.ConsoleTest
{
    class Program
    { 
        private static readonly ILogger logger = new NLoggerAdapter(LogManager.GetLogger(string.Empty));

        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                var bookService = new BookListService(logger);
                //bookService.AddBook(new Book("Dark Tower", "King", 1999, 30));
                //bookService.StoreBooksList(new BinaryBookListStorage("binaryStore", logger));
                bookService.LoadBooksList(new BinaryBookListStorage("binaryStore", logger));
                foreach (var book in bookService.GetListOfBooks())
                {
                    Console.WriteLine(book.ToString());
                }


            }
            catch (BookListException ex)
            {
                logger.Warn("An error occured during removing or saving in BookListService.", ex.InnerException);
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal("Unhandled exception: {0}", (Exception)e.ExceptionObject);
            NLogProvider.Flush();
        }
}


}
