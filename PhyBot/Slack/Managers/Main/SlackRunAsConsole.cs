using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Main
{
    public class SlackRunAsConsole : IDisposable
    {
        private AutoResetEvent _ShutdownEvent = new AutoResetEvent(false);
        public AutoResetEvent ShutdownEvent
        {
            get { return this._ShutdownEvent;  }
            set { this._ShutdownEvent = value;  }
        }

        private SlackMain SlackMain { get; set; }

        public SlackRunAsConsole(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Application");
                SlackMain = new SlackMain(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void Dispose()
        {
            SlackMain.Dispose();
            _ShutdownEvent.Set();
            GC.SuppressFinalize(this);
        }
    }
}
