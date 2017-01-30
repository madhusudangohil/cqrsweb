using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace cqrscommon
{
    public class Entity
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class EventRecord : Entity, ISerializable
    {
        private const string EntityKeyProperty = "_EntityKey";
        private const string EventKeyProperty = "_EventKey";
        private const string VersionProperty = "_VersionKey";
        private const string SystemTypeProperty = "_SystemTypeKey";
        private const string EventDataKeyProperty = "_EventDataKey";

        public Guid EntityKey { get; set; }
        public Guid EventKey { get; set; }

        public int Version { get; set; }

        public string SystemType { get; set; }
        public byte[] EventData { get; set; }

        public EventRecord()
        {

        }
        public EventRecord(SerializationInfo info, StreamingContext context)
        {
            EntityKey = (Guid)info.GetValue(EntityKeyProperty, typeof(Guid));
            EventKey = (Guid)info.GetValue(EventKeyProperty, typeof(Guid));

            Version = (int)info.GetValue(VersionProperty, typeof(int));
            SystemType = (string)info.GetValue(SystemTypeProperty, typeof(string));

            EventData = (byte[])info.GetValue(EventDataKeyProperty, typeof(byte[]));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(EntityKeyProperty, EntityKey, typeof(Guid));
            info.AddValue(EventKeyProperty, EventKey, typeof(Guid));
            info.AddValue(VersionProperty, Version, typeof(int));
            info.AddValue(SystemTypeProperty, SystemType, typeof(string));
            info.AddValue(EventDataKeyProperty, EventData, typeof(byte[]));
        }
    }

}
