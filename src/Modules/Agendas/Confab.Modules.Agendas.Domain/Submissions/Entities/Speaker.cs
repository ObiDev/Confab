using Confab.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public class Speaker : AggregateRoot
    {
        public string FullName { get; init; }

        public IEnumerable<Submission> Submissions => _submissions;

        private ICollection<Submission> _submissions;

        public Speaker(AggregateId id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public static Speaker Create(Guid id, string fullName)
        {
            return new Speaker(id, fullName);
        }
    }
}
