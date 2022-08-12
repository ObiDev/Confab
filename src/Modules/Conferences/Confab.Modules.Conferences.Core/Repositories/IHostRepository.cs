using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Rpository
{
    public interface IHostRepository
    {
        Task<Host> GetAsync(Guid id);
        Task<IReadOnlyList<Host>> BrowseAync();
        Task AddAsync(Host host);
        Task UpdateAsync(Host host);
        Task DeleteAsync(Host host);
    }
}
