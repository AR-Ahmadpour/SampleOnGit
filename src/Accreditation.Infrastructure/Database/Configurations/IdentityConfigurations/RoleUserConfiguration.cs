using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations.IdentityConfigurations
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.ToTable("RoleUser");
            builder.HasKey(roleuser => roleuser.Id);

            builder
                 .HasOne(ru => ru.Role)
                 .WithMany(r => r.RoleUsers)
                 .HasForeignKey(ru => ru.RolesId);

            builder
                .HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UsersGUID);


        }
    }
}
