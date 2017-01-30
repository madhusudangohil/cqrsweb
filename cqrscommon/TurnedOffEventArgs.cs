using System;
using System.Runtime.Serialization;
using inContact.DomainModel;

namespace cqrscommon
{
    [Serializable]
    public class TurnedOffEventArgs : EntityEventArgs
    {
        public TurnedOffEventArgs()
        {
            
        }
        public TurnedOffEventArgs(SerializationInfo info, StreamingContext context): base(info, context)
        {

        }
    }
}