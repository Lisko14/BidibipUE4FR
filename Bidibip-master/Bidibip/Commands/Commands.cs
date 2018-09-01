using Bidibip.Core;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidibip.Commands
{
    /// <summary>
    /// Core commands for Bidibip
    /// Created by Zino2201
    /// </summary>
    public class CoreCommands : ModuleBase
    {
        [Command("help"), Summary("Print a list of all commands")]
        public async Task Help()
        {
            string retMsg = "";

            retMsg += "ALL COMMANDS AVAILABLE FOR BIDIBIP\n";
            foreach (ModuleInfo info in BidibipBot.Instance.CommandHandler.Commands.Modules)
            {
                retMsg += "**[" + string.Concat(info.Name.
                    Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ') + "]**\n";

                foreach (CommandInfo cmdInfo in BidibipBot.Instance.CommandHandler.Commands.Commands)
                    if (cmdInfo.Module.Name == info.Name)
                        retMsg += string.Format("\t- {0}\t\t{1}\n", cmdInfo.Name, cmdInfo.Summary);
            }

            var user = Context.Message.Author;
            await UserExtensions.SendMessageAsync(user, retMsg);
        }

        [Command("quit"), Summary("Stop the bot (admin only)")]
        public async Task Quit()
        {
            var user = Context.Message.Author;
            if (((user as SocketGuildUser) as IGuildUser).GuildPermissions.Administrator)
            {
                await UserExtensions.SendMessageAsync(user, "Bot stopped!");
                await BidibipBot.Instance.Client.LogoutAsync();
                Environment.Exit(0);
            }
            else
                await UserExtensions.SendMessageAsync(user, ":no_entry_sign: You can't do that");
        }
    }
}
