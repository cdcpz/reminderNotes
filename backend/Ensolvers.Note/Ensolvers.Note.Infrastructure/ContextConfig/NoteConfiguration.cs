using Ensolvers.Note.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Infrastructure.ContextConfig
{
    public class NoteConfiguration : IEntityTypeConfiguration<Domain.Note>
    {
        public void Configure(EntityTypeBuilder<Domain.Note> NoteConfiguration)
        {

            //Address value object persisted as owned entity in EF Core 2.0
            //NoteConfiguration.OwnsOne(o => o.Credential);
            NoteConfiguration.Navigation(note => note.Tags).AutoInclude();

            //contactConfiguration.Property<DateTime>("OrderDate").IsRequired();

            //...Additional validations, constraints and code...
            //...
        }
    }
}
