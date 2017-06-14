﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;

namespace AgentTransferBot
{
    public class AgentToUserRouter : IAgentToUser
    {
        private readonly IAgentService _agentService;

        public AgentToUserRouter(IAgentService agentService)
        {
            _agentService = agentService;
        }
        public async Task SendToUserAsync(Activity message, CancellationToken cancellationToken)
        {
            var user = await _agentService.GetUserInConversationAsync(message, cancellationToken);
            var reference = user.ConversationReference;
            var reply = reference.GetPostToUserMessage();
            reply.Text = message.Text;

            await Utilities.SendToConversationAsync(reply);
        }
    }
}
