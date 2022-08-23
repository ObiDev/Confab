using Confab.Modules.Speakers.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Services
{
    internal interface ISpeakerService
    {
        Task AddAsync(SpeakerDto dto);
        Task<SpeakerDto> GetAsync(Guid id);
        Task<IEnumerable<SpeakerDto>> BrowseAync();
        Task UpdateAsync(SpeakerDto dto);
        Task DeleteAsync(Guid id);
    }
}
