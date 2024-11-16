using Accreditation.Domain.EnvironmentStandards.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations
{
    internal sealed class EnvironmentStandardConfiguration : IEntityTypeConfiguration<EnvironmentStandard>
    {
        public void Configure(EntityTypeBuilder<EnvironmentStandard> builder)
        {
            builder.ToTable("EnvironmentStandard");

            builder.HasKey(x => x.GUID);

            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.SpecialWeightedCoefficient);

            builder
                .HasOne(x => x.EtebarDoreh)
                .WithMany()
                .HasForeignKey(x => x.EtebarDorehGUID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
