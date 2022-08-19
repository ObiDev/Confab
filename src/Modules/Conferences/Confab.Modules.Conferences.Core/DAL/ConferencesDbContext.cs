using Confab.Modules.Conferences.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.DAL
{
    internal class ConferencesDbContext : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Host> Hosts { get; set; }

        public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: "conferences");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
