using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Represents behavior of a book
    /// </summary>
    public class Book: ICloneable, IFormattable, IEquatable<Book>, IComparable, IComparable<Book>
    {
        #region Constructors

        /// <summary>
        /// Constructs a book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if one of the parameters is invalid</exception>
        public Book(string name, string author, short year, decimal price)
        {
            Author = author;
            Name = name;
            Year = year;
            Price = price;
        }

        /// <summary>
        /// Constructs a book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if one of the parameters is invalid</exception>
        public Book(): this(string.Empty, string.Empty, default(short), default(decimal))
        { }

        #endregion

        #region Properties

        public string Author { get; }

        public string Name { get; }

        public short Year { get; }

        public decimal Price { get; }

        #endregion

        #region Public Methods

        public object Clone() => new Book(Name, Author, Year, Price);

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";
            if (ReferenceEquals(formatProvider, null))
                formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "NAYP": return string.Format(formatProvider, "{0}, {1}, {2}, {3:N}", Name, Author, Year, Price);
                default: throw new FormatException($"The {format} is not supported.");
            }
        }

        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            if (other.GetType() != this.GetType())
                return false;

            return Name.Equals(other.Name) && Author.Equals(other.Author) && Year.Equals(other.Year) && Price.Equals(other.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Book)obj);
        }

        public override int GetHashCode() => (Name.GetHashCode() + 13) ^ Author.GetHashCode();

        /// <summary>
        /// Uses <see cref="CompareTo"/>
        /// </summary>
        /// <exception cref="ArgumentException">Throws if 
        /// <paramref name="obj"/> cannot be casted to <see cref="Book"/></exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="obj"/> is
        /// not a type of <see cref="Book"/></exception>
        int IComparable.CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
                return 1;

            Book otherBook = obj as Book;
            if (!ReferenceEquals(otherBook, null))
                return CompareTo(otherBook);

            throw new ArgumentNullException($"Object is not a {typeof(Book)}");
        }

        /// <summary>
        /// Compare books by <see cref="Name"/>, if equals then by 
        /// <see cref="Author"/>, if equals then by <see cref="Year"/>,
        /// if equals then by <see cref="Price"/>.
        /// </summary>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            return Price.CompareTo(other.Price);
        }

        #endregion

    }
}
