using Accreditation.Domain.Fields.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("Field");

        builder.HasKey(x => x.GUID);
        builder.Property(x => x.Title).HasMaxLength(250).HasComment("رشته تخصصی ");
        builder.Property(x => x.TitleCode).HasMaxLength(250).HasComment("کد انگلیسی ");
        builder.Property(x => x.InstanceTypeIds).HasMaxLength(250).HasComment("لیستی از شناسه های نوع ارزیابی جدا شده توسط" +
                                                                             "کاما که این بسته ی خاص را در نوع ارزیابی خود استفاده می کنند");
        builder.Property(x => x.IsDeleted).HasComment("حذف شده");

        builder
            .HasOne(x => x.EtebarDoreh)
            .WithMany()
            .HasForeignKey(x => x.EtebarDorehGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
              .HasMany(e => e.EvaluationArzyabFields)
              .WithOne(f => f.Field)
              .HasForeignKey(e => e.FieldGuid)
              .OnDelete(DeleteBehavior.NoAction);
    }
}
