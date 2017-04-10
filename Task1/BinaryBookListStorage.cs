using System;
using System.Collections.Generic;
using System.IO;
using Task1.LogAdapter;

namespace Task1
{
    public class BinaryBookListStorage: IBookListStorage
    {
        private readonly string fileName;
        private readonly ILogger logger;

        public BinaryBookListStorage(string fileName, ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BinaryBookListStorage));

            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");

            logger.Debug("{0} constructor is started.", this);
            this.fileName = fileName;
        }

        public IEnumerable<Book> LoadBookList()
        {
            logger.Debug("Loading from binary storage {0} is started.", fileName);
            var list = new List<Book>();

            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    using (var reader = new BinaryReader(fs))
                    {
                        logger.Debug("File {0} is opened, starting reading...", nameof(fileName));
                        while (reader.PeekChar() > -1)
                        {
                            list.Add(new Book(reader.ReadString(), reader.ReadString(), reader.ReadInt16(),
                                reader.ReadDecimal()));
                        }
                        logger.Debug("Books from binary storage {0} added to {1}", nameof(fileName), nameof(list));
                    }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "An error occured during reading data from the {1}: {0}", nameof(fileName));
                throw new BookListStorageException($"An error occured during reading data from the {nameof(fileName)}", ex);
            }
            return list;
        }

        public void StoreBookList(IEnumerable<Book> list)
        {
            logger.Debug("Saving {0} to {1}", list, fileName);
            try
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    using (var writer = new BinaryWriter(fs))
                    {
                        logger.Debug("{0} is opened for writing.", fileName);
                        foreach (var book in list)
                        {
                            writer.Write(book.Name);
                            writer.Write(book.Author);
                            writer.Write(book.Year);
                            writer.Write(book.Price);
                        }
                        logger.Debug("Books from {0} wrote to {1}.", list, fileName);
                    }

            }
            catch (Exception ex)
            {
                logger.Warn("An error occured during writing data to the {1}: {0}", ex, fileName);
                throw new BookListStorageException($"An error occured during writing data to the {nameof(fileName)}", ex);
            }
        }
    }
}
