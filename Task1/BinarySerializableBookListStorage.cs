using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Task1.LogAdapter;

namespace Task1
{
    public class BinarySerializableBookListStorage : IBookListStorage
    {
        private readonly string fileName;
        private readonly ILogger logger;

        public BinarySerializableBookListStorage(string fileName, ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BinaryBookListStorage));

            logger.Debug("{0} constructor is started.", this);
            this.logger = logger;
            this.fileName = fileName;
        }

        public List<Book> LoadBookList()
        {
            logger.Debug("Loading from binary storage {0} is started.", fileName);
            List<Book> list;
            IFormatter formatter = new BinaryFormatter();

            try
            {
                using (FileStream s = File.OpenRead(fileName))
                {
                    logger.Debug("File {0} is opened, starting deserializing...", nameof(fileName));
                    list = (List<Book>)formatter.Deserialize(s);
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
            if (ReferenceEquals(list, null))
                throw new ArgumentNullException();

            IFormatter formatter = new BinaryFormatter();

            try
            {
                using (FileStream s = File.Create(fileName))
                {
                    formatter.Serialize(s, list);
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
