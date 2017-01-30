using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using cqrscommon;
using cqrswebwritedataacccess;


namespace cqrswritewebapi.events
{
    public interface IEventStore
    {
        void Write(EventRecord record);
        IReadOnlyCollection<EventRecord> Read(Guid key);
    }

    public class EventStore : IEventStore
    {
        WriteDataContext context = new WriteDataContext("TVCQRSDataContext");
        public void Write(EventRecord record)
        {
            context.Create(record);
            context.Commit();
        }

        public IReadOnlyCollection<EventRecord> Read(Guid key)
        {
            return context.Query<EventRecord>().Where(x => x.EntityKey == key).ToList();
        }
    }
}
