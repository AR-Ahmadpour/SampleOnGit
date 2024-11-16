using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccInstanceZirMehvarConfiguration : IEntityTypeConfiguration<AccInstanceZirMehvar>
{
    public void Configure(EntityTypeBuilder<AccInstanceZirMehvar> builder)
    {
        builder.ToTable("AccInstanceZirMehvar");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.AccreditationInstanceGUID);
        builder.Property(x => x.AccInstanceMehvarGUID);
        builder.Property(x => x.ZirMehvarGUID);
        builder.Property(x => x.LevelOneScore).HasComment("نتایج الزامی");
        builder.Property(x => x.LevelOnePossibleScore).HasComment("نتایج ممکن الزامی");
        builder.Property(x => x.LevelTwoAsasiScore).HasComment("نتایج اساسی");
        builder.Property(x => x.LevelTwoPossibleScore).HasComment("نتایج ممکن اساسی");
        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ایده آل");
        builder.Property(x => x.LevelThreePossibleScore).HasComment("نتایج ممکن ایده آل");

        builder
            .HasOne(x => x.ZirMehvar)
            .WithMany()
            .HasForeignKey(x => x.ZirMehvarGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AccreditationInstance)
            .WithMany()
            .HasForeignKey(x => x.AccreditationInstanceGUID)
            .OnDelete(DeleteBehavior.Cascade);
       
        builder
             .HasOne(x => x.AccInstanceMehvar)
             .WithMany()
             .HasForeignKey(x => x.AccInstanceMehvarGUID)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
