using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Book: ICloneable, IFormattable, IEquatable<Book>, IComparable, IComparable<Book>
    {
        public Book(string name, string author, short year, decimal price)
        {
            Author = author;
            Name = name;
            Year = year;
            Price = price;
        }

        public Book(): this(string.Empty, string.Empty, default(short), default(decimal))
        { }

        public string Author { get; set; }

        public string Name { get; set; }

        public short Year { get; set; }

        public decimal Price { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        public bool Equals(Book other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Book)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Book other)
        {
            throw new NotImplementedException();
        }
    }
}
