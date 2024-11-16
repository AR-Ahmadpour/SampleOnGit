using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations.IdentityConfigurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Title).IsRequired().HasMaxLength(100);

        builder.Property(role => role.Description).HasMaxLength(300);

        //builder.HasMany(role => role.RoleUsers)
        // .WithMany();



        builder.HasMany(role => role.RoleUsers)
            .WithOne(ru => ru.Role)
            .HasForeignKey(ru => ru.RolesId);

        //builder.HasMany(role => role.RolePermissions)
        //    .WithMany()
        //    .UsingEntity<RolePermission>();




        //builder.HasData(Role.Registered);
    }
}
