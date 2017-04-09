using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class BinaryBookListStorage: IBookListStorage
    {
        private string fileName;
        private ILogger logger;

        public BinaryBookListStorage(string fileName, ILogger logger)
        {
            if (ReferenceEquals(fileName, null))
                throw new ArgumentNullException();

            this.logger = logger;
            this.fileName = fileName;
        }

        public IEnumerable<Book> LoadBookList()
        {
            var list = new List<Book>();

            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(fs))
                    while (reader.PeekChar() > -1)
                        list.Add(new Book(reader.ReadString(), reader.ReadString(), reader.ReadInt16(), reader.ReadDecimal()));
            }
            catch (Exception ex)
            {
                throw new BookListStorageException($"An error occured during reading data from the {nameof(fileName)}", ex);
            }
            return list;
        }

        public void StoreBookList(IEnumerable<Book> list)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                using (var writer = new BinaryWriter(fs))
                    foreach (var book in list)
                    {
                        writer.Write(book.Author);
                        writer.Write(book.Name);
                        writer.Write(book.Price);
                        writer.Write(book.Year);
                    }

            }
            catch (Exception ex)
            {
                throw new BookListStorageException($"An error occured during writing data to the {nameof(fileName)}", ex);
            }
        }
    }
}
