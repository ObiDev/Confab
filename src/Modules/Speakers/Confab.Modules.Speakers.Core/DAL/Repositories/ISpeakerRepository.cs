using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal interface ISpeakerRepository
    {
        Task AddAsync(Speaker speaker);
        Task<Speaker> GetAsync(Guid id);
        Task<IReadOnlyList<Speaker>> BrowseAync();
        Task UpdateAsync(Speaker speaker);
        Task DeleteAsync(Speaker id);
        Task<bool> ExistsAsync(Guid id);
    }
}
