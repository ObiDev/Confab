using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Repositories
{
    internal interface ISpeakerRepository
    {
        Task<bool> ExistsAsync(AggregateId id);
        Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
        Task CreateAsync(Speaker speaker);

    }
}
