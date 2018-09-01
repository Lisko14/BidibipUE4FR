using Bidibip.Utils;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Bidibip.Core
{
    /// <summary>
    /// Bidibip Bot main class
    /// Created by Zino2201
    /// </summary>
    public class BidibipBot
    {
        /** Plugins folder name */
        public const string PLUGINS_DIR = "Plugins";

        public string CommandPrefix { get; private set; }

        public const string BIDIBIP_VERSION = "0.1";

        /** Instance of the bot class */
        public static BidibipBot Instance { get; private set; }
        
        /** Current configuration file name */
        public string ConfigFile { get; private set; }

        /** Current logger */
        public Logger Logger { get; private set; }

        /** Discord bot instance */
        public DiscordSocketClient Client { get; private set; }

        public EventHandler EventHandler;

        public CommandHandler CommandHandler;

        public IServiceProvider Services;

        public List<Assembly> Modules = new List<Assembly>();

        public BidibipBot(string configFile)
        {
            Instance = this;

            this.Client = new DiscordSocketClient();
            this.Logger = new Logger();
            this.EventHandler = new EventHandler();
            this.CommandHandler = new CommandHandler();
            this.ConfigFile = configFile;

            ConfigReader.Initialize(ConfigFile);

            this.CommandPrefix = ConfigReader.Read("Prefix", "Main");

            Logger.Log("Bidibip v{0}", BIDIBIP_VERSION);
            Logger.Log("Current config file: {0}", ConfigFile);
            Logger.Log("Current token: {0}", ConfigReader.Read("Token", "Main"));
            Logger.Log("Current command prefix: {0}", CommandPrefix);

            Modules.Add(Assembly.GetEntryAssembly());
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (string dll in Directory.GetFiles(path, "Modules\\*.dll"))
            {
                Logger.Log("Loaded module {0}", dll);
                Modules.Add(Assembly.LoadFile(dll));
            }
        }

        public async Task Start()
        {
            await Initialize();
        }

        private async Task Initialize()
        {
            await Logger.Log("Connecting to bot user...");
            Client.Log += Logger.Log;
            Client.MessageReceived += EventHandler.OnMessageReceived;

            Services = new ServiceCollection()
                .BuildServiceProvider();

            await Client.LoginAsync(TokenType.Bot, ConfigReader.Read("Token", "Main"));
            await Client.StartAsync();

            await CommandHandler.Initialize();

            await Task.Delay(-1);
        }
    }
}
