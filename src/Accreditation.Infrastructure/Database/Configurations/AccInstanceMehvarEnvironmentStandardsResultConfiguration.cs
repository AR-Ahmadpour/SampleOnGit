
using Accreditation.Domain.AccInstanceMehvarEnvironmentStandardsResults.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccInstanceMehvarEnvironmentStandardsResultConfiguration : IEntityTypeConfiguration<AccInstanceMehvarEnvironmentStandardsResult>
{
    public void Configure(EntityTypeBuilder<AccInstanceMehvarEnvironmentStandardsResult> builder)
    {
        builder.ToTable("AccInstanceMehvarEnvironmentStandardsResult");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.AccInstanceMehvarGUID);

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
            .HasOne(x => x.AccInstanceMehvar)
            .WithMany()
            .HasForeignKey(x => x.AccInstanceMehvarGUID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
