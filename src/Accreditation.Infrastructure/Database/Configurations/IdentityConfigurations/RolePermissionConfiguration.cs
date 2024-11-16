using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace Accreditation.Infrastructure.Database.Configurations.IdentityConfigurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermission");

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });


        builder
           .HasOne(rp => rp.Role)
           .WithMany(r => r.RolePermissions)
           .HasForeignKey(rp => rp.RoleId);

        builder
        .HasOne(rp => rp.Permission)
        .WithMany(p => p.RolePermissions)
        .HasForeignKey(rp => rp.PermissionId);

    }
}

