using Accreditation.Domain.HamkariModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations
{
    internal class HamkariModelConfiguration : IEntityTypeConfiguration<HamkariModel>
    {
        public void Configure(EntityTypeBuilder<HamkariModel> builder)
        {
            builder.ToTable("HamkariModel");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();

            builder.Property(x => x.SortOrder).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
