using Accreditation.Domain.AccreditationInstanceStatusTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;
internal sealed class AccreditationInstanceStatusTypeConfiguration : IEntityTypeConfiguration<AccreditationInstanceStatusType>
{
    public void Configure(EntityTypeBuilder<AccreditationInstanceStatusType> builder)
    {

        builder.ToTable("AccreditationInstanceStatusType");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(250).HasComment("وضعیت");
        builder.Property(x => x.Code).HasMaxLength(50).HasComment("کد انگلیسی وضعیت");
        builder.Property(x => x.IsDestinyStatus);
        builder.Property(x => x.IsLocked);
        builder.Property(x => x.IsFinalStatus);
        builder.Property(x => x.StepOrder);
    }
}
