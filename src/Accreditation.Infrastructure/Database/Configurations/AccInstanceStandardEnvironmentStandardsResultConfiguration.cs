using Accreditation.Domain.AccInstanceStandardEnvironmentStandardsResults.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccInstanceStandardEnvironmentStandardsResultConfiguration : IEntityTypeConfiguration<AccInstanceStandardEnvironmentStandardsResult>
{
    public void Configure(EntityTypeBuilder<AccInstanceStandardEnvironmentStandardsResult> builder)
    {
        builder.ToTable("AccInstanceStandardEnvironmentStandardsResult");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.AccInstanceStandardGUID);

        builder.Property(x => x.EnvironmentStandardGUID);

        builder.Property(x => x.LevelOneScore).HasComment("نتایج الزامی");

        builder.Property(x => x.LevelOnePossibleScore).HasComment("نتایج ممکن الزامی");

        builder.Property(x => x.LevelTwoScore).HasComment("نتایج اساسی");

        builder.Property(x => x.LevelTwoPossibleScore).HasComment("نتایج ممکن اساسی");

        builder.Property(x => x.LevelThreeScore).HasComment("نتایج ایده آل");

        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ممکن ایده آل");

        builder
            .HasOne(x => x.EnvironmentStandard)
            .WithMany()
            .HasForeignKey(x => x.EnvironmentStandardGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AccInstanceStandard)
            .WithMany()
            .HasForeignKey(x => x.AccInstanceStandardGUID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
