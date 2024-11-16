using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Accreditation.Infrastructure.Database.Configurations.IdentityConfigurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(permission => permission.Id);

        builder.HasOne(permission => permission.category).WithMany()
            .HasForeignKey(permission => permission.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
       // builder.HasData(Permission.UsersRead);
    }
}

