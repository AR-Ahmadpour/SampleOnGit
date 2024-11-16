using Accreditation.Domain.AccreditationInstancesEnvironmentStandardsResults.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccreditationInstancesEnvironmentStandardsResultConfiguration : IEntityTypeConfiguration<AccreditationInstancesEnvironmentStandardsResult>
{
    public void Configure(EntityTypeBuilder<AccreditationInstancesEnvironmentStandardsResult> builder)
    {
        builder.ToTable("AccreditationInstancesEnvironmentStandardsResult");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.LevelOneScore).IsRequired(false).HasComment("نتایج الزامی");

        builder.Property(x => x.LevelOnePossibleScore).IsRequired(false).HasComment("نتایج ممکن الزامی");

        builder.Property(x => x.LevelTwoScore).IsRequired(false).HasComment("نتایج اساسی");

        builder.Property(x => x.LevelTwoPossibleScore).IsRequired(false).HasComment("نتایج ممکن اساسی");

        builder.Property(x => x.LevelThreeScore).IsRequired(false).HasComment("نتایج ایده آل");

        builder.Property(x => x.LevelThreePossibleScore).IsRequired(false).HasComment("نتایج ممکن ایده آل");

        builder
            .HasOne(x => x.EnvironmentStandard)
            .WithMany()
            .HasForeignKey(x => x.EnvironmentStandardGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AccreditationInstance)
            .WithMany()
            .HasForeignKey(x => x.AccreditationInstanceGUID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
