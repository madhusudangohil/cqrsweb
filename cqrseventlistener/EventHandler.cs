using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using inContact.DomainModel;

namespace cqrseventlistener
{
    public interface IHandler<TEvent> where TEvent : EventArgs
    {
        void Handle(TEvent args);
    }
    public class EventHandler<TEvent> : IHandler<TEvent> where TEvent : EventArgs
    {
        private readonly Denormalizer _denormalizer;

        public EventHandler(Denormalizer denormalizer)
        {
            _denormalizer = denormalizer;
        }
        public void Handle(TEvent args)
        {
            MethodInfo[] methods = _denormalizer.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.InvokeMethod);
            foreach (MethodInfo method in methods)
            {
                EntityEventHandlerAttribute attribute = method.GetCustomAttribute<EntityEventHandlerAttribute>();
                if (attribute != null && attribute.CanHandle(args.GetType()))
                {
                    method.Invoke(_denormalizer, new object[] { args });
                }
            }
        }
    }
}
