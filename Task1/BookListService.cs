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
            if(!ReferenceEquals(logger, null))
                throw new ArgumentNullException();
            this.logger = logger;
            list = new List<Book>();
        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book FindBookByTag(Predicate<Book> predicate)
        {
            throw new NotImplementedException();
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
