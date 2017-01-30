using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cqrscommon;
using inContact.DomainModel;

namespace cqrseventlistener
{
    public partial class EventListener : ServiceBase
    {
        private bool _terminate;
        private bool _CommandLineMode;

        public EventListener()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var th = new Thread(new ThreadStart(ReadQueue));
            th.Start();
        }

        protected override void OnStop()
        {
            _terminate = true;
        }

        private void ReadQueue()
        {
            while(!_terminate)
                EventReader.ReadQueue();
        }

        static void Main(string[] args)
        {
            var service = new EventListener();
            var clArg = (from arg in args
                         where arg.Equals("/commandline", StringComparison.OrdinalIgnoreCase)
                         select arg).Any();
            if (clArg)
            {
                service.RunAsCommandLine(args);
            }
            else
            {
                service.RunAsService();

            }
        }

        private void RunAsCommandLine(string[] args)
        {
            _CommandLineMode = true;
            this.OnStart(args);
            Console.ReadKey(false);
            this.OnStop();
        }

        private void RunAsService()
        {
            _CommandLineMode = false;
            var ServicesToRun = new ServiceBase[] { this };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
