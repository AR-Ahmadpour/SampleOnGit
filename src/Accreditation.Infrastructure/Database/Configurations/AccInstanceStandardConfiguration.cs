using Accreditation.Domain.AccInstanceStandards.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccInstanceStandardConfiguration : IEntityTypeConfiguration<AccInstanceStandard>
{
    public void Configure(EntityTypeBuilder<AccInstanceStandard> builder)
    {
        builder.ToTable("AccInstanceStandard");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.AccreditationInstanceGUID);
        builder.Property(x => x.AccInstanceZirMehvarGUID);
        builder.Property(x => x.StandardGUID);
        builder.Property(x => x.LevelOneScore).HasComment("نتایج الزامی");
        builder.Property(x => x.LevelOnePossibleScore).HasComment("نتایج ممکن الزامی");
        builder.Property(x => x.LevelTwoScore).HasComment("نتایج اساسی");
        builder.Property(x => x.LevelTwoPossibleScore).HasComment("نتایج ممکن اساسی");
        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ایده آل");
        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ممکن ایده آل");

        builder
            .HasOne(x => x.Standard)
            .WithMany()
            .HasForeignKey(x => x.StandardGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AccreditationInstance)
            .WithMany()
            .HasForeignKey(x => x.AccreditationInstanceGUID)
            .OnDelete(DeleteBehavior.Cascade);

        //builder
        //    .HasOne(a => a.AccreditationInstance)
        //    .WithMany(b => b.AccInstanceStandards) // Ensure this navigation property is defined
        //    .HasForeignKey(a => a.AccreditationInstanceGUID)
        //    .OnDelete(DeleteBehavior.Cascade);

        builder
             .HasOne(x => x.AccInstanceZirMehvar)
             .WithMany()
             .HasForeignKey(x => x.AccInstanceZirMehvarGUID)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
