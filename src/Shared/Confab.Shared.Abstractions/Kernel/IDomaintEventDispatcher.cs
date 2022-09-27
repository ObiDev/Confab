using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Kernel
{
    public interface IDomaintEventDispatcher
    {
        Task DispatchAsync(params IDomainEvent[] events);
    }
}
