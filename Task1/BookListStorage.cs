using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class BookListStorage : IBookListStorage
    {
        private string fileName;
        
        public IEnumerable<Book> LoadBookList()
        {
            throw new NotImplementedException();
        }

        public void StoreBookList(IEnumerable<Book> list)
        {
            throw new NotImplementedException();
        }
    }
}
