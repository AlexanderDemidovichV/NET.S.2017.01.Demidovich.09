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
        public string ResourceReferenceProperty { get; set; }

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
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (ReferenceEquals(info, null))
                throw new ArgumentNullException("info");
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
