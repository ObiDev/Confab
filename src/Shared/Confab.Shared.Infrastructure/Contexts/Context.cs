using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal class Context : IContext
    {
        public string RequestId { get; } = $"{Guid.NewGuid():N}";
        public string TraceId { get; }
        public IIdentityContext Identity { get; }

        internal Context()
        {
        }

        public Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
        {

        }

        public Context(string traceId, IIdentityContext identity)
        {
            TraceId = traceId;
            Identity = identity;
        }

        public static IContext Empty => new Context();
    }
}
