using Confab.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal sealed class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> _message = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => _message.Reader;
        public ChannelWriter<IMessage> Writer => _message.Writer;
    }
}
