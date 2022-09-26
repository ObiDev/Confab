using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class ConferenceId : TypeId
    {
        public ConferenceId(Guid value) : base(value)
        {
        }

        public static implicit operator ConferenceId(Guid id) => new ConferenceId(id);
    }
}
