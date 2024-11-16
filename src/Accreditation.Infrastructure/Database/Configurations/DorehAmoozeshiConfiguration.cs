using Accreditation.Domain.DorehAmoozeshis.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Accreditation.Infrastructure.Database.Configurations
{
    internal sealed class DorehAmoozeshiConfiguration : IEntityTypeConfiguration<DorehAmoozeshi>
    {
        public void Configure(EntityTypeBuilder<DorehAmoozeshi> builder)
        {

            builder.ToTable("DorehAmoozeshi");

            builder.HasKey(x => x.GUID);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.SortOrder).IsRequired();

        }
    }
}
