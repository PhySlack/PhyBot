using PhyBot.Slack.Managers.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot
{
    /// <summary>
    /// This is the PhyBotWindowsService. We use this if we want to  run this program as a Windows Service.
    /// </summary>
    partial class PhyBotWindowsService : ServiceBase
    {
        /// <summary>
        /// Arguments passed on Startup
        /// </summary>
        private string[] Args { get; set; }

        /// <summary>
        /// SlackMain
        /// </summary>
        private SlackMain SlackMain { get; set; }

        /// <summary>
        /// Constructor for PhyBotWindowsService
        /// </summary>
        public PhyBotWindowsService(string[] args)
        {
            InitializeComponent();
            Args = args;
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-console")
            {
                using (SlackRunAsConsole slackRunAsConsole = new SlackRunAsConsole(args))
                {
                    slackRunAsConsole.ShutdownEvent.WaitOne();
                }
            }
            else
            {
                // run as windows service
                ServiceBase[] serviceBases = new ServiceBase[]
                {
                    new PhyBotWindowsService(args)
                };
                Run(serviceBases);
            }
        }

        /// <summary>
        /// On Service Start
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            Console.WriteLine("OnStart called");
            SlackMain = new SlackMain(args);
        }

        /// <summary>
        /// On Service Stop
        /// </summary>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Console.WriteLine("OnStop called");
            SlackMain.Dispose();
        }
    }
}
