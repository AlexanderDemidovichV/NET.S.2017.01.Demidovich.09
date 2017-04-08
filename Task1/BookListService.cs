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
        private IList<Book> list;

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

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            throw new NotImplementedException();
        }

        public void StoreBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException();
            try
            {
                storage.StoreBookList(list);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void LoadBooksList(IBookListStorage storage)
        {
            if (ReferenceEquals(storage, null))
                throw new ArgumentNullException();
            IEnumerable<Book> loadedBooks;
            try
            {
                loadedBooks = storage.LoadBookList();
            }
            catch (Exception e)
            {
                throw;
            }
            if (ReferenceEquals(loadedBooks, null))
                throw new NotImplementedException();
            list = (List<Book>)loadedBooks;
        }
    }
}
