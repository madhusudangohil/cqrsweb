using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using inContact.DomainModel;

namespace cqrscommon
{
    [Serializable]
    public class TelevisionInitialized : EntityEventArgs
    {
        private const string KeyProperty = "_EntityKey";
        public Guid EntityKey { get; set; }
        public TelevisionInitialized(Guid key)
        {
            EntityKey = key;
        }

        public TelevisionInitialized(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            EntityKey = (Guid)info.GetValue(KeyProperty, typeof(Guid));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(KeyProperty, EntityKey);
            base.GetObjectData(info, context);
        }
    }
}
