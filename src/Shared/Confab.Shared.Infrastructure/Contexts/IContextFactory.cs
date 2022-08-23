using Confab.Shared.Abstractions.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}
