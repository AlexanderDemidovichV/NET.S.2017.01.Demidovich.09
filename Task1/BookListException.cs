using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    [Serializable]
    public class BookListException : Exception
    {
        public BookListException()
        {
        }

        public BookListException(string message) : base(message)
        {
        }

        public BookListException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BookListException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}
