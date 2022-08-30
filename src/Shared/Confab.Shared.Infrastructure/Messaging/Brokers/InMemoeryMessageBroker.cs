using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    internal sealed class InMemoeryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;

        public InMemoeryMessageBroker(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
                return;

            var tasks = new List<Task>();

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
                return;

            foreach (var message in messages)
            {
                tasks.Add(_moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}
