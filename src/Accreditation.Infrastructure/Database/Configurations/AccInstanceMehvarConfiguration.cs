
using Accreditation.Domain.AccInstanceMehvars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccInstanceMehvarConfiguration : IEntityTypeConfiguration<AccInstanceMehvar>
{
    public void Configure(EntityTypeBuilder<AccInstanceMehvar> builder)
    {
        builder.ToTable("AccInstanceMehvar");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.AccreditationInstanceGUID);

        builder.Property(x => x.MehvarGUID);

        builder.Property(x => x.LevelOneScore).HasComment("نتایج الزامی");

        builder.Property(x => x.LevelOnePossibleScore).HasComment("نتایج ممکن الزامی");

        builder.Property(x => x.LevelTwoScore).HasComment("نتایج اساسی");

        builder.Property(x => x.LevelTwoPossibleScore).HasComment("نتایج ممکن اساسی");

        builder.Property(x => x.LevelThreeScore).HasComment("نتایج ایده آل");

        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ممکن ایده آل");

        builder
            .HasOne(x => x.Mehvar)
            .WithMany()
            .HasForeignKey(x => x.MehvarGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AccreditationInstance)
            .WithMany()
            .HasForeignKey(x => x.AccreditationInstanceGUID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
