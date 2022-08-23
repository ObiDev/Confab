using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Contexts
{
    public interface IContext
    {
        string RequestId { get; }
        string TraceId { get; }
        IIdentityContext Identity { get; }
    }
}
