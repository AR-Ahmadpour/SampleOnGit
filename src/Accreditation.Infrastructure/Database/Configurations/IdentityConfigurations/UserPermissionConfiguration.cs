using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Database.Configurations.IdentityConfigurations
{
    internal sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {

        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission");

            builder
                .HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserGUID)
                 .OnDelete(DeleteBehavior.NoAction); 

            //builder
            //    .HasOne(up => up.CreateByUser)
            //    .WithMany()
            //    .HasForeignKey(up => up.CreateByUser)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(up => up.UpdateByUser)
            //    .WithMany()
            //    .HasForeignKey(up => up.UpdateByGUID)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
