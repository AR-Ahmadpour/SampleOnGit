using Accreditation.Domain.EtebarDorehs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class EtebarDorehConfiguration : IEntityTypeConfiguration<EtebarDoreh>
{
    public void Configure(EntityTypeBuilder<EtebarDoreh> builder)
    {
        builder.ToTable("EtebarDoreh");
 
        builder.HasKey(x => x.GUID) ;

        builder.Property(x => x.Title).HasMaxLength(1000).IsRequired().HasComment("عنوان دوره اعتباربخشی");

        builder.Property(x => x.IsDeleted).HasMaxLength(200).HasComment("حذف شده");

        builder.Property(x => x.CreationDate).HasComment("زمان ایجاد");

        builder.Property(x => x.UpdateDate).HasComment("زمان آخرین ویرایش ");

        builder.Property(x => x.CreatedByGUID).HasComment("ایجاد توسط");

        builder.Property(x => x.UpdatedByGUID).HasComment("آخرین ویرایش توسط ");

        builder.Property(x => x.StartDate).HasComment("زمان شروع");

        builder.Property(x => x.EndDate).HasComment("زمان خاتمه");

        builder.Property(x => x.IsCurrent).HasComment("دوره جاری");

        builder.Property(x => x.SortOrder).HasComment("ترتیب مرتبسازی");

        builder.Property(x => x.StartDate).HasComment("ترتیب");

        builder.HasOne(x => x.OrgType)
            .WithMany()
            .HasForeignKey(x => x.OrgTypeGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ed => ed.CreatedByUser)
            .WithMany()
            .HasForeignKey(ed => ed.CreatedByGUID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ed => ed.UpdatedByUser)
            .WithMany()
            .HasForeignKey(ed => ed.UpdatedByGUID)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
