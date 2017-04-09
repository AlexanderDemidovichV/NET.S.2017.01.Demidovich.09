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
    public class BookListStorageException: Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public BookListStorageException()
        {
        }

        public BookListStorageException(string message) : base(message)
        {
        }

        public BookListStorageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BookListStorageException(SerializationInfo info, StreamingContext context)
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
