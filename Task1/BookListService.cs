using System;
using System.Collections.Generic;
using Task1.LogAdapter;

namespace Task1
{
    public class BookListService
    {
        private readonly ILogger logger;
        private List<Book> list;

        public BookListService(ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BookListService));

            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");

            logger.Debug(($"Constructor {nameof(BookListService)} with one parameter is started."));

            list = new List<Book>();
        }

        public BookListService(ILogger logger, IEnumerable<Book> list)
        {
            if (ReferenceEquals(list, null))
                throw new ArgumentNullException($"{nameof(list)} is null.");

            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BookListService));

            logger.Debug(($"Constructor {nameof(BookListService)} with two parameters is started."));

            this.list = new List<Book>(list);
        }

        public IEnumerable<Book> GetListOfBooks()
        {
            logger.Debug("Returning enumeration of books.");
            return list.ToArray();
        }

        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null))
                throw new ArgumentNullException();

            logger.Debug("Adding {0}.", book);
            if (list.Contains(book))
            {
                logger.Info("An error occured during adding {0} to the book list, {0} already exist in the list.", book);
                throw new BookListException(
                    $"An error occured during adding {nameof(book)} from the book list, {nameof(book)} already exist in the list.");
            }
            list.Add(book);

        }

        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null))
                throw new ArgumentNullException();

            logger.Debug("Removing book {0}.", book);
            if (!list.Remove(book))
            {
                logger.Info("An error occured during removing {0} from the book list.", book);
                throw new BookListException($"An error occured during removing {nameof(book)} from the book list.");
            }
        }

        public Book FindBookByTag(Predicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
                throw new ArgumentNullException();

            logger.Debug("Searching for book by tag {0}", predicate);
            return (Book)list.Find(predicate).Clone();
        }

        public void SortBookListByTag(IComparer<Book> comparer)
        {
            if (ReferenceEquals(comparer, null))
                throw new ArgumentNullException();

            logger.Debug("Sorting book list by comparer {0}", comparer);
            list.Sort(comparer);
        }

        public void SortBookListByTag(Comparison<Book> comparison)
        {
            if (ReferenceEquals(comparison, null))
                throw new ArgumentNullException();

            logger.Debug("Sorting book list by coparison {0}", comparison);
            list.Sort(Comparer<Book>.Create(comparison));
        }

        public void StoreBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException();
            try
            {
                logger.Debug("Saving book list.");
                storage.StoreBookList(list);
            }
            catch (Exception ex)
            {
                logger.Warn("An error has occurred during saving book's list to storage.", ex);
                throw new BookListException($"An error has occurred during saving book's list to {nameof(storage)}.", ex);
            }
        }

        public void LoadBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException($"{nameof(storage)} is null");

            logger.Debug("Loading book list from {0}", nameof(storage));
            IEnumerable<Book> loadedBooks;

            try
            {
                loadedBooks = storage.LoadBookList();
            }
            catch (Exception ex)
            {
                logger.Warn("An error has occurred during loading book's list from {1}: {0}.", ex, storage);
                throw new BookListException($"An error has occurred during loading book's list from {nameof(storage)}.", ex);
            }

            if (ReferenceEquals(loadedBooks, null))
            {
                logger.Warn("LoadBookList returned null.");
                throw new BookListException($"{nameof(LoadBooksList)} returned null.");
            }
            list = (List<Book>)loadedBooks;
        }
    }
}
