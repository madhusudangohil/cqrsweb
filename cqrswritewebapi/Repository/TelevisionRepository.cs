using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using cqrscommon;
using cqrswritewebapi.Aggregate;
using cqrswritewebapi.events;
using inContact.DomainModel;
using inContact.Messaging;
using cqrswebwritedataacccess;

namespace cqrswritewebapi.Repository
{
    /// <summary>Retrieves and stores Television enties.</summary>
    public class TelevisionRepository
    {
       
        private readonly IEventStore _eventStore;

        /// <summary>Rerieves an instance of Television for the specified key.</summary>
        /// <param name="key">A value that uniquely identifies the television.</param>
        /// <returns>Returns an instance of <see cref="Television"/>.</returns>
        public Television Find(Guid key)
        {
            var records = _eventStore.Read(key);
            var history = records.Select<EventRecord, EntityEventArgs>(record => record.EventData.FromBinary<EntityEventArgs>());
            Television entity = new Television(key);
            foreach (var historicEvent in history)
            {
                entity.EntityState.Load(historicEvent);
            }
            return entity;
        }

        public void Save(Television entity)
        {
            foreach (var eventArgs in entity.EntityState.Changes)
            {
                var change = (EntityEventArgs) eventArgs;
                var record = new EventRecord
                {
                    EntityKey = entity.Key,
                    EventKey = change.Key,
                    Version = entity.EntityState.Version,
                    SystemType = entity.GetType().AssemblyQualifiedName,
                    EventData = change.ToBinary()
                };
                _eventStore.Write(record);
            }
        }

        /// <summary>Creates a new instance of <see cref="TelevisionRepository"/>.</summary>
        public TelevisionRepository() : this(new EventStore())
        {

        }

        public TelevisionRepository(IEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException("eventStore");
            }
            _eventStore = eventStore;
        }
    }

    public static class Extensions
    {
        public static T FromBinary<T>(this byte[] eventData)
        {
            T args;
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(eventData))
            {
                args = (T)formatter.Deserialize(ms);
            }
            return args;
        }

        public static byte[] ToBinary(this EntityEventArgs args)
        {
            byte[] binary;
            var formatter = new BinaryFormatter();
            
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, args);
                ms.Position = 0;
                binary = ms.GetBuffer();
                return binary;
            }
            
        }
    }
}
