using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.LogAdapter;

namespace Task1
{
    /// <summary>
    /// Interface of abstract <see cref="Book"/> storage
    /// </summary>
    public interface IBookListStorage
    {
        /// <summary>
        /// Stores <paramref name="books"/> somewhere
        /// </summary>
        void StoreBookList(IEnumerable<Book> list);

        /// <summary>
        /// Loads <see cref="Book"/>s enumerable from somewhere
        /// </summary>  
        IEnumerable<Book> LoadBookList();
    }
}
