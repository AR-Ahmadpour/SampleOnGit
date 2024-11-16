using Accreditation.Domain.CountryDivisions.BakhshLocation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class BakhshLocationConfiguration : IEntityTypeConfiguration<Bakhsh>
{
    public void Configure(EntityTypeBuilder<Bakhsh> builder)
    {
        builder.ToTable("Bakhsh");

        builder.HasKey(x => x.Id);
       

        builder.Property(x => x.IntCode).HasMaxLength(50).HasComment("کد یکپارچه سازی");

        builder.Property(x => x.Title).HasMaxLength(150).HasComment("نام بخش  ");

        builder.Property(x => x.IsDeleted).HasComment("حذف منطقی");

        builder.Property(x => x.SortOrder).HasComment("ترتیب");

        builder.Property(x => x.ShahrestanId).HasComment("شناسه شهرستان");

        builder.HasOne(x => x.Shahrestan)
            .WithMany()
            .HasForeignKey(x => x.ShahrestanId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
