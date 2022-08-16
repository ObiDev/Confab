using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryConferenceRepository : IConferenceRepository
    {
        //No thread safe, better to use concurrent;
        private readonly List<Conference> _conferences = new List<Conference>();

        public Task AddAsync(Conference conference)
        {
            _conferences.Add(conference);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Conference>> BrowseAync()
        {
            await Task.CompletedTask;
            return _conferences;
        }

        public Task DeleteAsync(Conference conference)
        {
            _conferences.Remove(conference);
            return Task.CompletedTask;
        }

        public Task<Conference> GetAsync(Guid id)
        {
            var host = _conferences.SingleOrDefault(c => c.Id == id);
            return Task.FromResult(host);
        }

        public Task UpdateAsync(Conference conference)
        {
            return Task.CompletedTask;
        }
    }
}
