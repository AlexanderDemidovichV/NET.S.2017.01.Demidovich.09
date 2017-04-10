using System;
using NLog;
using Task1.LogAdapter;
using Task1.LogProvider;
using ILogger = Task1.LogAdapter.ILogger;

namespace Task1.ConsoleTest
{
    class Program
    {
        public static readonly ILogger logger = NLogProvider.GetLogger("Pro");

        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                var bookService = new BookListService(logger);
                //bookService.AddBook(new Book("Dark Tower", "King", 1999, 30));
                //bookService.StoreBooksList(new BinaryBookListStorage("binaryStore", logger));

                var binaryBookListStorage = new BinaryBookListStorage("binaryStore", logger);
                bookService.LoadBooksList(binaryBookListStorage);
                foreach (var book in bookService.GetListOfBooks())
                {
                    Console.WriteLine(book.ToString());
                }

            }
            catch (BookListException ex)
            {
                logger.Warn("An error occured during removing or saving in BookListService.", ex.InnerException);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occured");
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal((Exception)e.ExceptionObject, "Unhandled exception.");
            NLogProvider.Flush();
        }
}


}
