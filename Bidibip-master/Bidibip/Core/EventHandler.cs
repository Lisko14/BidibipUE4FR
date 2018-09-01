using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidibip.Core
{
    /// <summary>
    /// Event handler
    /// Created by Zino2201
    /// </summary>
    public class EventHandler
    {
        // TODO: Attribute event for modules

        public async Task OnMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot)
                return;

            if (message.Content.StartsWith(BidibipBot.Instance.CommandPrefix))
                await BidibipBot.Instance.CommandHandler.OnCommandExecuted(message);
        }
    }
}
