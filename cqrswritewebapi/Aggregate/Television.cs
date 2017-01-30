using System;
using System.Collections.Generic;
using cqrswritewebapi.events;
using inContact.DomainModel;
using cqrscommon;

namespace cqrswritewebapi.Aggregate
{
    /// <summary>Represents a television appliance.</summary>
    public class Television : AggregateRoot
    {
        // NOTE:  There are no public properties. The state of the object is private (encapsulated) & only represents 
        //              business logic needs. This object cannot be querried for its state. 
        private int CurrentChannel;
        private int PreviousChannel;
        private bool IsOn;

        /// <summary>Occurs when the channel is changed.</summary>
        /// <param name="e">Contains information about the channel changed event.</param>
        [EntityEventHandler(typeof(ChannelChangedEventArgs))]
        private void OnChannelChanged(ChannelChangedEventArgs e)
        {
            PreviousChannel = e.From;
            CurrentChannel = e.To;
        }

        /// <summary>Occurs when the television is turned on.</summary>
        [EntityEventHandler(typeof(TurnedOnEventArgs))]
        protected virtual void OnTurnedOn(TurnedOnEventArgs e)
        {
            IsOn = true;
        }

        /// <summary>Occurs when the television is turned off.</summary>
        [EntityEventHandler(typeof(TurnedOffEventArgs))]
        protected virtual void OnTurnedOff(TurnedOffEventArgs e)
        {
            IsOn = false;
        }

        /// <summary>Changes the television channel to the specified value.</summary>
        /// <param name="channel">A positive number representing the desired channel.</param>
        /// <exception cref="InvalidOperationException">The television is not powerd on.</exception>
        /// <exception cref="ArgumentException">The channel value is less than one.</exception>
        public void ChangeChannel(int channel)
        {
            if (!IsOn)
            {
                throw new InvalidOperationException("The television must be turned on in order to change the channel.");
            }
            if (channel < 1)
            {
                throw new ArgumentException("The channel cannot be set to less than one.");
            }
            ChannelChangedEventArgs e = new ChannelChangedEventArgs(CurrentChannel, channel);
            EntityState.Apply(e);
        }

        /// <summary>Changes the channel to the previous channel.</summary>
        /// <exception cref="InvalidOperationException">The television is not powerd on.</exception>
        public void RecallChannel()
        {
            if (!IsOn)
            {
                throw new InvalidOperationException("The television must be turned on in order to change the channel.");
            }
            ChannelChangedEventArgs e = new ChannelChangedEventArgs(CurrentChannel, PreviousChannel);
            EntityState.Apply(e);
        }

        /// <summary>Turns the television on.</summary>
        /// <exception cref="InvalidOperationException">The television is currently on.</exception>
        public void TurnOn()
        {
            if (IsOn)
            {
                throw new InvalidOperationException("The television is currently on and cannot be turned on again.");
            }
            TurnedOnEventArgs e = new TurnedOnEventArgs();
            EntityState.Apply(e);
        }

        /// <summary>Turns the television off.</summary>
        /// <exception cref="InvalidOperationException">The television is not powerd on.</exception>
        public void TurnOff()
        {
            if (!IsOn)
            {
                throw new InvalidOperationException("The television is not on and cannot be turned off.");
            }
            TurnedOffEventArgs e = new TurnedOffEventArgs();
            EntityState.Apply(e);
        }

        /// <summary>Creates a new instance of <see cref="Television"/>.</summary>
        public Television() : this(Guid.NewGuid())
        { 
            TelevisionInitialized e = new TelevisionInitialized(base.Key);
            EntityState.Apply(e);
        }

        /// <summary>Creates a new instance of <see cref="Television"/> identified by the specified key.</summary>
        /// <param name="key">A unique value that identifies this instance.</param>
        public Television(Guid key) : base(key)
        { 
        
        }
    }
}
