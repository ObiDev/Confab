using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Rpository
{
    internal class InMemoryHostRepository : IHostRepository
    {
        //No thread safe, better to use concurrent;
        private readonly List<Host> _hosts = new List<Host>();

        public Task AddAsync(Host host)
        {
            _hosts.Add(host);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Host>> BrowseAync()
        {
            await Task.CompletedTask;
            return _hosts;
        }

        public Task DeleteAsync(Host host)
        {
            _hosts.Remove(host);
            return Task.CompletedTask;
        }

        public Task<Host> GetAsync(Guid id)
        {
            var host = _hosts.SingleOrDefault(h => h.Id == id);
            return Task.FromResult(host);
        }

        public Task UpdateAsync(Host host)
        {
            return Task.CompletedTask;
        }
    }
}
