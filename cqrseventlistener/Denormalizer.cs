using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using cqrscommon;
using inContact.DomainModel;
using inContact.Messaging;

namespace cqrseventlistener
{
    public class Denormalizer
    {
        private Television _tvState;
        ReadStroreUpdateContext dataContext = new ReadStroreUpdateContext("readStoreUpdater");
        
        
        public Denormalizer(Guid id)
        {
            _tvState = new Television() {Id = id};
        }

        /// <summary>Occurs when the channel is changed.</summary>
        /// <param name="e">Contains information about the channel changed event.</param>
        [EntityEventHandler(typeof(ChannelChangedEventArgs))]
        public void OnChannelChanged(ChannelChangedEventArgs e)
        {
            _tvState.FromChannel = e.From;
            _tvState.ToChannel = e.To;
            _tvState.Version = e.Version;
            dataContext.Update(_tvState, x => x.FromChannel, x => x.ToChannel, x => x.Version);
            dataContext.Commit();
            Console.WriteLine("ChannelChangedEventArgs");
        }

        /// <summary>Occurs when the television is turned on.</summary>
        [EntityEventHandler(typeof(TurnedOnEventArgs))]
        public virtual void OnTurnedOn(TurnedOnEventArgs e)
        {
            _tvState.IsOn = true;
            _tvState.Version = e.Version;
            dataContext.Update(_tvState, x => x.IsOn, x => x.Version);
            dataContext.Commit();
            Console.WriteLine("TurnedOnEventArgs");
        }

        /// <summary>Occurs when the television is turned off.</summary>
        [EntityEventHandler(typeof(TurnedOffEventArgs))]
        public virtual void OnTurnedOff(TurnedOffEventArgs e)
        {
            _tvState.IsOn = false;
            _tvState.Version = e.Version;
            dataContext.Update(_tvState, x=>x.IsOn, x=>x.Version);
            dataContext.Commit();
            Console.WriteLine("TurnedOffEventArgs");
        }

        [EntityEventHandler(typeof(TelevisionInitialized))]
        public virtual void OnTelevisionInitialized(TelevisionInitialized e)
        {
            _tvState.Id = e.EntityKey;
            _tvState.Version = e.Version;
            dataContext.Update(_tvState);
            dataContext.Commit();
            Console.WriteLine("TelevisionInitialized");
        }
    }
}
