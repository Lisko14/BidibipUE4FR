using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bidibip.Utils;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bidibip.Core
{
    /// <summary>
    /// Command Handler class
    /// Created by Zino2201
    /// </summary>
    public class CommandHandler
    {
        public CommandService Commands;

        public CommandHandler()
        {
            this.Commands = new CommandService();
        }

        public async Task Initialize()
        {
            foreach(Assembly module in BidibipBot.Instance.Modules)
                await Commands.AddModulesAsync(module);
        }

        public async Task OnCommandExecuted(SocketMessage message)
        {
            await Logger.Log("Command executed from {0} ({1}) in channel {2}",
                message.Author.Username, message.Content, message.Channel.Name);

            SocketUserMessage msg = message as SocketUserMessage;
            CommandContext context = new CommandContext(BidibipBot.Instance.Client, msg);

            var result = await Commands.ExecuteAsync(context, 1, BidibipBot.Instance.Services);
            await message.DeleteAsync();

            if (!result.IsSuccess)
            {
                await (message.Author.GetOrCreateDMChannelAsync() as IMessageChannel)
                    .SendMessageAsync(":no_entry_sign: " + result.ErrorReason);
                await Logger.Log("Command failed: {0}", result.ErrorReason);
            }
        }
    }
}
