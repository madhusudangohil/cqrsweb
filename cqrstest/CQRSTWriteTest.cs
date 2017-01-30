using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrseventlistener;
using cqrswritewebapi.commands;
using NUnit.Framework;

namespace cqrstest
{
    [TestFixture]
    public class CQRSTWriteTest
    {
        [Test]
        public void TestTVInitializeEvent()
        {
            var commandHandler = new CommandHandler();
            commandHandler.Handle(new InitializeTvCommand());
        }

        [Test]
        public void TestEventQueue()
        {
            EventReader.ReadQueue();
        }

        [Test]
        public void InChannelChange()
        {

            var commandHandler = new CommandHandler();
            var id = Guid.Parse("6695BA3A-76CC-4A56-8232-54BE7AA67F24");

            var version = 142;
            for (int i = 58; i < 10000; i++)
            {
                commandHandler.Handle(new ChangeChannelCommand() { ChannelNumber = ++i, Id = id, Version = ++version });
            }
            
        }

    }
}
