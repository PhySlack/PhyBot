using PhyBot.Slack.Configurations;
using PhyBot.Slack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.RMTSession
{
    public class RMTSessionManager
    {
        private string ContentType = "application/x-www-form-urlencoded";
        private HTTPManager HTTPManager = new HTTPManager();

        public void StartSession()
        {
            try
            {
                string url = @"	https://slack.com/api/rtm.connect";
                //string newURL = string.Format("{0}?token={1}&pretty=1", url, token);
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    { "token", ConfigurationManager.Instance.Token },
                    { "pretty", "1" },
                };

                var HttpCustomResponse = HTTPManager.GetRequest(url, ContentType, parameters);

                if (HttpCustomResponse.SuccessResponse == Models.SuccessResponse.Successful)
                {
                    Console.WriteLine(HttpCustomResponse.Body);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }

}
