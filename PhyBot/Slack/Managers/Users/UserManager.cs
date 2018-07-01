using PhyBot.Slack.Configurations;
using PhyBot.Slack.Http;
using PhyBot.Slack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PhyBot.Slack.Managers.Users
{
    public class UserManager
    {
        private string ContentType = "application/x-www-form-urlencoded";
        private HTTPManager HTTPManager = new HTTPManager();

        // holds users
        //public void GetUsers()
        //{
        //    try
        //    {
        //        string url = @"	https://slack.com/api/users.list";
        //        //string  = string.Format("{0}?token={1}&pretty=1", url, ConfigurationManager.Token);
        //        Dictionary<string, string> parameters = new Dictionary<string, string>()
        //        {
        //            {  "token", "ConfigurationManager.Token" },
        //            {  "pretty", "1" },
        //        };

        //        var response = HTTPManager.GetRequest(url, ContentType, parameters);
        //    }
        //    catch (Exception exception)
        //    {

        //    }
        //}

        public UserResponse GetUser(string userId)
        {
            UserResponse userResponse = null;
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return userResponse;
                }

                string url = @"https://slack.com/api/users.info";
                //string  = string.Format("{0}?token={1}&user={2}&pretty=1", url, ConfigurationManager.Token, userId);
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    {  "token", ConfigurationManager.Instance.Token },
                    {  "user", userId },
                    {  "pretty", "1" },
                };

                var response = HTTPManager.GetRequest(url, ContentType, parameters);

                if (response.SuccessResponse == SuccessResponse.Successful)
                {
                    userResponse = new JavaScriptSerializer().Deserialize<UserResponse>(response.Body);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return userResponse;
        }
    }
}