using System;
using System.Runtime.Serialization;
using inContact.DomainModel;

namespace cqrscommon
{
    [Serializable]
    public class TurnedOnEventArgs : EntityEventArgs
    {
        public TurnedOnEventArgs()
        {
            
        }

        public TurnedOnEventArgs(SerializationInfo info, StreamingContext context): base(info, context)
        {
            
        }
       
    }
}