using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Configurations
{
    public class ConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static ConfigurationManager _ConfigurationManager { get; set; }

        // Read your token out of a config file
        public string Token = string.Empty;

        // config file name
        public const string FileName = @"PhyBot.config";

        /// <summary>
        /// Returns the ConfigurationManager instance
        /// </summary>
        public static ConfigurationManager Instance
        {
            get
            {
                if (_ConfigurationManager == null)
                {
                    _ConfigurationManager = new ConfigurationManager();
                }
                return _ConfigurationManager;
            }
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private ConfigurationManager()
        {
            // private constructor
        }

        // read configs
        public void ReadConfigs()
        {
            try
            {
                string executableFilePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string executableDirectory = System.IO.Path.GetDirectoryName(executableFilePath);
                string configFilePath = string.Format(@"{0}\{1}", executableDirectory, FileName);
                if (File.Exists(configFilePath))
                {
                    string text = System.IO.File.ReadAllText(configFilePath);
                    Token = text;
                    Console.WriteLine(string.Format("Token {0}", Token));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
