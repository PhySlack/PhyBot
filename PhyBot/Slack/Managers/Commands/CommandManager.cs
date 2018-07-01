using PhyBot.Slack.Managers.Main;
using PhyBot.Slack.Managers.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Commands
{
    public class CommandManager
    {
        public void ProcessCommand(string text, string channelName, UserResponse UserResponse)
        {
            var lowerCaseText = text.ToLower();

            if (lowerCaseText.StartsWith(".phybot"))
            {
                try
                {
                    var stringBuilder = new StringBuilder();
                    var slackManager = SlackManager.Instance;

                    // Global level permission to use the command feature
                    bool isAllowed = ValidatePermissions(UserResponse);
                    if (isAllowed == false)
                    {
                        var reply = string.Format("User {0} has No Permissions to use .phybot.", UserResponse.User.Profile.Display_Name);
                        Console.WriteLine(reply);
                        slackManager.SendSlackMessage(channelName, reply);
                        return;
                    }

                    switch (lowerCaseText)
                    {
                        #region Help
                        case "":
                        case @".phybot help":
                            {
                                // display some useful commands
                                slackManager.SendSlackMessage(channelName, ".phybot commands --to see list of commands.");
                                break;
                            }
                        case @".phybot commands":
                            {
                                // display all commands
                                slackManager.SendSlackMessage(channelName, ".phybot time-uct --returns time in UCT");
                                slackManager.SendSlackMessage(channelName, ".phybot test-reply --phybot repeats what you write.");
                                break;
                            }
                        #endregion

                        #region actions
                        case @".phybot time-uct":
                            {
                                var reply = string.Format("The current UCT time is: {0:H:mm:ss}", DateTime.UtcNow);
                                slackManager.SendSlackMessage(channelName, reply);
                                break;
                            }
                        case @".phybot time":
                            {
                                stringBuilder.AppendLine(string.Format("The current UCT time is: {0:MM/dd/yy H:mm:ss}", DateTime.UtcNow));
                                stringBuilder.AppendLine(string.Format("The current New York time is: {0:MM/dd/yy H:mm:ss}", GetDateTimeByTimeZoneId(@"Eastern Standard Time\Dynamic DST")));
                                stringBuilder.AppendLine(string.Format("The current Sydney time is: {0:MM/dd/yy H:mm:ss}", GetDateTimeByTimeZoneId(@"AUS Eastern Standard Time\Dynamic DST")));
                                var reply = stringBuilder.ToString();
                                slackManager.SendSlackMessage(channelName, reply);
                                break;
                            }
                        case @".phybot test-reply":
                            {
                                var reply = string.Format(text);
                                slackManager.SendSlackMessage(channelName, reply);
                                break;
                            }
                        case @".phybot test-exception":
                            {
                                throw new Exception("test-exception");
                            }
                        #endregion

                        #region not recognised command
                        default:
                            {
                                // display some useful commands
                                var reply = string.Format("Command not recognised. Please use .phybot help");
                                slackManager.SendSlackMessage(channelName, reply);
                                break;
                            }
                            #endregion
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    var reply = string.Format("Oops .phybot was unable to process your command. Error: {0}", exception.Message);
                    SlackManager.Instance.SendSlackMessage(channelName, reply);
                }
            }
        }

        private bool ValidatePermissions(UserResponse UserResponse)
        {
            bool success = true;
            // this user only has permissions
            if (UserResponse.User.Id != "UBHT4V6NB")
            {
                success = false;
            }
            return success;
        }

        private DateTime GetDateTimeByTimeZoneId(string timeZoneId = "GMT Standard Time")
        {
            try
            {
                DateTime localtime = DateTime.Now;
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime dataTimeByZoneId = TimeZoneInfo.ConvertTime(localtime, System.TimeZoneInfo.Local, timeZoneInfo);
                return dataTimeByZoneId;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return DateTime.UtcNow;
        }
    }
}