using System;
using System.Collections.Generic;
using Task1.LogAdapter;

namespace Task1
{
    /// <summary>
    /// Provides functionality to work with a list of books
    /// </summary>
    public class BookListService
    {
        private readonly ILogger logger;
        private List<Book> list;

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class
        /// </summary>
        public BookListService(ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BookListService));

            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");

            logger.Debug("Constructor {0} with one parameter is started.", nameof(BookListService));

            list = new List<Book>();
        }

        public BookListService(ILogger logger, IEnumerable<Book> list)
        {
            if (ReferenceEquals(list, null))
                throw new ArgumentNullException($"{nameof(list)} is null.");

            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BookListService));

            logger.Debug("Constructor {0} with two parameters is started.", nameof(BookListService));

            this.list = new List<Book>(list);
        }

        /// <summary>
        /// Returns an enumeration of <see cref="Book"/>s in service
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetListOfBooks()
        {
            logger.Debug("Returning enumeration of books.");
            return list.ToArray();
        }

        /// <summary>
        /// Adds a book to the book list
        /// </summary>
        /// <exception cref="BookListException">Throws if <paramref name="book"/> is already in
        /// the book list</exception>
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

        /// <summary>
        /// Removes a book from the book list
        /// </summary>
        /// <exception cref="BookListException">Throws if the book is already removed</exception>
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

        /// <summary>
        /// Finds a book that gives true from <paramref name="predicate"/>.
        /// Do not change the instance by returned reference!
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/>
        /// is null</exception>
        public Book FindBookByTag(Predicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
                throw new ArgumentNullException();

            logger.Debug("Searching for book by tag {0}", predicate);
            return (Book)list.Find(predicate).Clone();
        }


        /// <summary>
        /// Sorts books in the book list according to <paramref name="comparer"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/>
        /// is null</exception>
        public void SortBookListByTag(IComparer<Book> comparer)
        {
            if (ReferenceEquals(comparer, null))
                throw new ArgumentNullException();

            logger.Debug("Sorting book list by comparer {0}", comparer);
            list.Sort(comparer);
        }

        /// <summary>
        /// Sorts books in the book list according to <paramref name="comparison"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparison"/>
        /// is null</exception>
        public void SortBookListByTag(Comparison<Book> comparison)
        {
            if (ReferenceEquals(comparison, null))
                throw new ArgumentNullException();

            logger.Debug("Sorting book list by coparison {0}", comparison);
            list.Sort(Comparer<Book>.Create(comparison));
        }

        /// <summary>
        /// Stores books to <paramref name="storage"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="storage"/>
        ///  is null</exception>
        /// <exception cref="BookListException">Throws if some errors while storing books
        /// if <paramref name="storage"/></exception>
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
                logger.Warn(ex, "An error has occurred during saving book's list to storage.");
                throw new BookListException($"An error has occurred during saving book's list to {nameof(storage)}.", ex);
            }
        }

        /// <summary>
        /// Loads books from <paramref name="storage"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="storage"/>
        /// is null</exception>
        /// <exception cref="BookListException">Throws if some errors while
        /// loading books from <paramref name="storage"/></exception>
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
                logger.Warn(ex, "An error has occurred during loading book's list from {0}.", nameof(storage));
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
