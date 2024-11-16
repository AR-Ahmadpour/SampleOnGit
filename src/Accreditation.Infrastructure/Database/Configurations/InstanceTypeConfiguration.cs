using Accreditation.Domain.InstanceType.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class InstanceTypeConfiguration : IEntityTypeConfiguration<InstanceType>
{
    public void Configure(EntityTypeBuilder<InstanceType> builder)
    {
        builder.ToTable("InstanceType");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive);
        builder.Property(x => x.GUID);
        builder.Property(x => x.Title).HasMaxLength(100).HasComment("عنوان نوع ارزیابی ");
        builder.Property(x => x.IsActiveInUniversity);
        builder.Property(x => x.IsActiveInStaff);
    }
}
