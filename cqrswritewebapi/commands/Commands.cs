using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cqrswritewebapi.commands
{

    public class Command
    {
        public int Version { get; set; }
    }

    public class InitializeTvCommand : Command
    {
        
    }

    public class PowerOnTvCommand : Command
    {
        public Guid Id { get; set; }
    }

    public class PowerOffTvCommand : Command
    {
        public Guid Id { get; set; }
    }

    public class ChangeChannelCommand : Command
    {
        public Guid Id { get; set; }
        public int ChannelNumber { get; set; }
    }

}