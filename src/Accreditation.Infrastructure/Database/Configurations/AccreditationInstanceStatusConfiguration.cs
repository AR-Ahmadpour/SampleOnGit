using Accreditation.Domain.AccreditationInstanceStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccreditationInstanceStatusConfiguration : IEntityTypeConfiguration<AccreditationInstanceStatus>
{
    public void Configure(EntityTypeBuilder<AccreditationInstanceStatus> builder)
    {
        builder.ToTable("AccreditationInstanceStatus");

        builder.HasKey(x => x.GUID);
        builder.Property(x => x.UserGUID);
        builder.Property(x => x.ChangeStatusDate);
        builder.Property(x => x.AccreditationInstanceStatusTypeId);
        builder.Property(x => x.AccreditationInstanceGUID);

    }
}
