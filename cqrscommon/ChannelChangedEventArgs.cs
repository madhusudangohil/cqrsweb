using System;
using System.Runtime.Serialization;
using inContact.DomainModel;

namespace cqrscommon
{
    [Serializable]
    public class ChannelChangedEventArgs : EntityEventArgs
    {
        private const string FromProperty = "_From";
        private const string ToProperty = "_To";
        public int From { get; internal set; }
        public int To { get; internal set; }

        public ChannelChangedEventArgs(int from, int to)
        {
            From = from;
            To = to;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(FromProperty, From);
            info.AddValue(ToProperty, To);
            base.GetObjectData(info, context);
        }

        public ChannelChangedEventArgs()
        {
            
        }

        protected ChannelChangedEventArgs(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            From = (int)info.GetValue(FromProperty, typeof(int));
            To = (int)info.GetValue(ToProperty, typeof(int));
        }
    }
}