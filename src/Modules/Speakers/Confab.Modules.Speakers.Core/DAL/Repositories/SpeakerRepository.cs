using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakerRepository : ISpeakerRepository
    {
        private readonly SpeakersDbContext _context;
        private readonly DbSet<Speaker> _speakers;

        public SpeakerRepository(SpeakersDbContext context)
        {
            _context = context;
            _speakers = context.Speakers;
        }

        public async Task AddAsync(Speaker speaker)
        {
            await _speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Speaker>> BrowseAync()
             => await _speakers.ToListAsync();

        public async Task DeleteAsync(Speaker speaker)
        {
            _context.Remove(speaker);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
            => await _speakers.AnyAsync(s => s.Id == id);

        public Task<Speaker> GetAsync(Guid id)
            => _speakers.SingleOrDefaultAsync(s => s.Id == id);

        public async Task UpdateAsync(Speaker speaker)
        {
            _speakers.Update(speaker);
            await _context.SaveChangesAsync();
        }
    }
}
