using PhyBot.Slack.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Main
{
    public class SlackMain : IDisposable
    {
        public SlackMain(string[] args)
        {
            // Read configs
            ConfigurationManager.Instance.ReadConfigs();

            // Startup our SlackManager program
            var slackManager = SlackManager.Instance;
            
            // Start the RTMSession
            slackManager.StartRTMSession();

            // Stream the RTM
            slackManager.StreamSlack();
        }

        public void Dispose()
        {
            try
            {
                // Dispose our SlackManager here
                SlackManager.Instance.Dispose();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }

            GC.SuppressFinalize(this);
        }
    }
}
