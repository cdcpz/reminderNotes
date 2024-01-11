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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> TagConfiguration)
        {

            //Address value object persisted as owned entity in EF Core 2.0
            TagConfiguration.OwnsOne(o => o.Color);
            TagConfiguration.OwnsOne(o => o.BgColor);

            //contactConfiguration.Property<DateTime>("OrderDate").IsRequired();

            //...Additional validations, constraints and code...
            //...
        }
    }
}
