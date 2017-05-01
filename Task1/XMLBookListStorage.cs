using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Task1.LogAdapter;

namespace Task1
{
    public class XMLBookListStorage : IBookListStorage
    {
        private readonly string fileName;
        private readonly ILogger logger;

        public XMLBookListStorage(string fileName, ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                logger = LogProvider.NLogProvider.GetLogger(nameof(BinaryBookListStorage));

            logger.Debug("{0} constructor is started.", this);
            this.logger = logger;
            this.fileName = fileName;
        }

        public List<Book> LoadBookList()
        {
            logger.Debug("Loading from xml storage {0} is started.", fileName);
            var list = new List<Book>();
            
            try
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    XmlReader reader = new XmlTextReader(stream);
                    logger.Debug("File {0} is opened, starting reading...", nameof(fileName));

                    bool isEmpty = reader.IsEmptyElement;
                    reader.ReadStartElement();
                    if (isEmpty)
                        return null;

                    while (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == Book.xmlName)
                        {
                            reader.ReadStartElement();
                            string author = reader.ReadElementContentAsString("author", "");
                            string name = reader.ReadElementContentAsString("name", "");
                            decimal price = decimal.Parse(reader.ReadElementContentAsString("price", ""));
                            short year = short.Parse(reader.ReadElementContentAsString("year", ""));
                            reader.ReadEndElement();
                            list.Add(new Book(name, author, year, price));
                        }
                    }
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

            try
            {
                using (FileStream s = File.Create(fileName))
                {
                    XmlWriter writer = new XmlTextWriter(s, Encoding.Default);

                    foreach (var book in list)
                    {
                        writer.WriteStartElement(Book.xmlName);
                        writer.WriteElementString("author", book.Author);
                        writer.WriteElementString("name", book.Name);
                        writer.WriteElementString("price", book.Price.ToString());
                        writer.WriteElementString("year", book.Year.ToString());
                        writer.WriteEndElement();
                    }
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
