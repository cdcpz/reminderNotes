using Ensolvers.Note.Domain;
using Ensolvers.Note.Infrastructure.ContextConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Infrastructure
{
    public class NoteContext : DbContext
    {
        public DbSet<Domain.Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NoteContext(DbContextOptions<NoteContext> dbOptions) : base(dbOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Value objects persist config
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());

            base.OnModelCreating(modelBuilder);
            //...Additional type configurations
        }
    }
}
