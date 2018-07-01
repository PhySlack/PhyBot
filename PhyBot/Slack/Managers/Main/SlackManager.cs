using PhyBot.Slack.Configurations;
using PhyBot.Slack.Managers.Commands;
using PhyBot.Slack.Managers.RMTSession;
using PhyBot.Slack.Managers.Users;
using SlackAPI;
using SlackAPI.WebSocketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Main
{
    /// <summary>
    /// Slack Manager
    /// </summary>
    public class SlackManager : IDisposable
    {
        /// <summary>
        /// SlackManager
        /// </summary>
        private static SlackManager _SlackManager { get; set; }

        /// <summary>
        /// RMTSessionManager
        /// </summary>
        private RMTSessionManager RMTSessionManager = new RMTSessionManager();

        /// <summary>
        /// ManualResetEventSlim
        /// </summary>
        private ManualResetEventSlim ManualResetEventSlim { get; set; }

        /// <summary>
        /// SlackSocketClient
        /// </summary>
        private SlackSocketClient SlackSocketClient { get; set; }

        /// <summary>
        /// Returns the SlackManager instance
        /// </summary>
        public static SlackManager Instance
        {
            get
            {
                if (_SlackManager == null)
                {
                    _SlackManager = new SlackManager();
                }
                return _SlackManager;
            }
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private SlackManager()
        {
            // private constructor
        }

        /// <summary>
        /// Starts the RTM Session
        /// </summary>
        public void StartRTMSession()
        {
            RMTSessionManager.StartSession();
        }

        /// <summary>
        /// Start streaming slack
        /// </summary>
        public void StreamSlack()
        {
            try
            {
                var token = ConfigurationManager.Instance.Token;
                ManualResetEventSlim = new ManualResetEventSlim(false);
                SlackSocketClient = new SlackSocketClient(token);

                // Test connection
                //TestConnection(SlackSocketClient);

                // Connect
                SlackSocketClient.Connect((loginResponse) =>
                {
                    // This is called once the client has emitted the RTM start command
                    ManualResetEventSlim.Set();

                }, () => {
                    // This is called once the RTM client has connected to the end point
                    OnConnected(null);
                });

                // OnMessageReceived
                SlackSocketClient.OnMessageReceived += (newMessage) =>
                {
                    // Handle each message as you receive them
                    OnMessageReceived(newMessage);
                };

                //SlackSocketClient.GetUserList((u) =>
                //{
                //    Console.WriteLine(u);
                //});

                // Wait
                ManualResetEventSlim.Wait();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Try close the slack streaming connection
        /// </summary>
        private void TryCloseConnection()
        {
            try
            {
                SlackSocketClient.CloseSocket();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Test if we can connect
        /// </summary>
        /// <param name="slackSocketClient"></param>
        private void TestConnection(SlackSocketClient slackSocketClient)
        {
            slackSocketClient.TestAuth((testme) =>
            {
                Console.WriteLine("TestAuth");
                if (testme.ok == false)
                {
                    Console.WriteLine("Failed to connect");
                }
                else
                {

                }
            });
        }

        /// <summary>
        /// On Connect
        /// </summary>
        /// <param name="loginResponse"></param>
        private void OnConnected(LoginResponse loginResponse)
        {
            Console.WriteLine("Connected");
        }

        /// <summary>
        /// On Disconnect
        /// </summary>
        /// <param name="newMessage"></param>
        private void OnMessageReceived(NewMessage newMessage)
        {
            Console.WriteLine("NewMessage Received");

            if (newMessage == null)
            {
                return;
            }

            // Handle each message as you receive them
            Console.WriteLine(newMessage);

            if (newMessage.ok == false)
            {
                return;
            }

            var channel = newMessage.channel;
            var team = newMessage.team;
            var userId = newMessage.user;
            var timeStamp = newMessage.ts;


            UserManager userManager = new UserManager();
            var user = userManager.GetUser(userId);

            if (user == null)
            {
                return;
            }

            var channels = SlackSocketClient.Channels;
            var slackChannel = SlackSocketClient.Channels.Find(x => x.id.Equals(channel));

            //Console.WriteLine(string.Format("User: {0} has sent the message {1}", displayName, message.text));

            CommandManager commandManager = new CommandManager();
            commandManager.ProcessCommand(newMessage.text, slackChannel.name, user);
        }

        /// <summary>
        /// Send slack message
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="message"></param>
        public void SendSlackMessage(string channelName, string message)
        {
            try
            {
                SlackSocketClient.GetChannelList((clr) => { Console.WriteLine("got channels"); });

                var channels = SlackSocketClient.Channels;
                var slackChannel = SlackSocketClient.Channels.Find(x => x.name.Equals(channelName));
                if (slackChannel == null)
                {
                    Console.WriteLine("Channel does not exist");
                    return;
                }
                SlackSocketClient.PostMessage((mr) => Console.WriteLine(message), slackChannel.id, message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            TryCloseConnection();
            GC.SuppressFinalize(this);
        }
    }
}
