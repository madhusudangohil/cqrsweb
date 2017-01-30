using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using cqrscommon;
using cqrswritewebapi.Aggregate;
using cqrswritewebapi.events;
using cqrswritewebapi.Repository;
using inContact.DomainModel;

namespace cqrswritewebapi.Broker
{
    public interface IBroker<T>
    {
        /// <summary>Publish an item so that all subscribed listeners of that type will receive it.</summary>
        /// <param name="item">The item to publish.</param>
        void Publish(T item);
    }

    public class EventBroker : IBroker<Television>
    {
        private static Lazy<EventBroker> _Default = new Lazy<EventBroker>(() => new EventBroker());
        Random _random = new Random();
        
        public static EventBroker Default
        {
            get { return _Default.Value; }
        }
        public void Publish(Television item)
        {
            foreach (EntityEventArgs change in item.EntityState.Changes)
            {
                EventRecord record = new EventRecord();
                record.EntityKey = item.Key;
                record.EventKey = change.Key;
                record.Version = item.EntityState.Version;
                record.SystemType = item.GetType().AssemblyQualifiedName;
                record.EventData = change.ToBinary();
                using (FileStream fs = new FileStream(@"D:\Learning\TVCQRS\Queue\Event" + _random.Next(), FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, record);
                }
            }
            
        }
    }
}