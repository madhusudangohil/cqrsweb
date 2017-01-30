using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cqrswritewebapi.Aggregate;
using cqrswritewebapi.Broker;
using cqrswritewebapi.events;
using cqrswritewebapi.Repository;

namespace cqrswritewebapi.commands
{
    public interface ICommandHandler
    {
        void Handle(InitializeTvCommand initialized);
        void Handle(PowerOnTvCommand powerOn);
        void Handle(PowerOffTvCommand powerOff);
        void Handle(ChangeChannelCommand changeChannelCommand);

    }

    public class CommandHandler : ICommandHandler
    {
        private Television telivsion;
        TelevisionRepository _repository = new TelevisionRepository(new EventStore());
        private readonly IBroker<Television> _broker;

        public CommandHandler()
        {
            _broker = EventBroker.Default;
        }
        //Denormalizer dn = new Denormalizer();
        public void Handle(InitializeTvCommand initialized)
        {
            telivsion = new Television();
            _repository.Save(telivsion);
            _broker.Publish(telivsion);
            telivsion.EntityState.AcceptChanges();
        }

        public void Handle(PowerOnTvCommand powerOn)
        {
            telivsion = _repository.Find(powerOn.Id);
            telivsion.TurnOn();
            _repository.Save(telivsion);
            _broker.Publish(telivsion);
            telivsion.EntityState.AcceptChanges();
        }

        public void Handle(PowerOffTvCommand powerOff)
        {
            telivsion = _repository.Find(powerOff.Id);
            telivsion.TurnOff();
            _repository.Save(telivsion);
            _broker.Publish(telivsion);
            telivsion.EntityState.AcceptChanges();
        }

        public void Handle(ChangeChannelCommand changeChannelCommand)
        {
            telivsion = _repository.Find(changeChannelCommand.Id);
            telivsion.ChangeChannel(changeChannelCommand.ChannelNumber);
            _repository.Save(telivsion);
            _broker.Publish(telivsion);
            telivsion.EntityState.AcceptChanges();
        }
    }
}