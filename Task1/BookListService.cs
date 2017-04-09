using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class BookListService
    {
        private ILogger logger;
        private List<Book> list;

        public BookListService(ILogger logger)
        {
            if(ReferenceEquals(logger, null))
                throw new ArgumentNullException();
            this.logger = logger;
            list = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null))
                throw new ArgumentNullException();

            if (list.Contains(book))
                throw new BookListException($"An error occured during adding {nameof(book)} from the book list, {nameof(book)} already exist in the list.");
            list.Add(book);

        }

        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null))
                throw new ArgumentNullException();

            if (!list.Remove(book))
                throw new BookListException($"An error occured during removing {nameof(book)} from the book list.");
        }

        public Book FindBookByTag(Predicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
                throw new ArgumentNullException();

            return (Book)list.Find(predicate).Clone();
        }

        public void SortBookListByTag(IComparer<Book> comparer)
        {
            if (ReferenceEquals(comparer, null))
                throw new ArgumentNullException();

            list.Sort(comparer);
        }

        public void SortBookListByTag(Comparison<Book> comparison)
        {
            if (ReferenceEquals(comparison, null))
                throw new ArgumentNullException();

            list.Sort(Comparer<Book>.Create(comparison));
        }

        public void StoreBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException();
            try
            {
                storage.StoreBookList(list);
            }
            catch (Exception ex)
            {
                throw new BookListException($"An error has occurred during saving book's list to {nameof(storage)}.", ex);
            }
        }

        public void LoadBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException($"{nameof(storage)} is null");

            IEnumerable<Book> loadedBooks;

            try
            {
                loadedBooks = storage.LoadBookList();
            }
            catch (Exception ex)
            {
                throw new BookListException($"An error has occurred during loading book's list from {nameof(storage)}.", ex);
            }

            if (ReferenceEquals(loadedBooks, null))
                throw new BookListException($"{nameof(LoadBooksList)} returned null.");

            list = (List<Book>)loadedBooks;
        }
    }
}
