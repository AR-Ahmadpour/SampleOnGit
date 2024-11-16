using Accreditation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using System;

namespace Accrediation.Infrastructure.Database.Configurations.IdentityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(user => user.GUID);

        builder.Property(user => user.AtbaKhareji).IsRequired(false).HasComment("ملیت غیر ایرانی");
        builder.Property(user => user.NationalCode).IsRequired(false).HasMaxLength(10).HasComment("کد ملی");
        builder.Property(user => user.FirstName).IsRequired(false).HasMaxLength(150).HasComment("");
        builder.Property(user => user.LastName).IsRequired(false).HasMaxLength(150).HasComment("");
        builder.Property(user => user.FatherName).IsRequired(false).HasMaxLength(150).HasComment("");
        builder.Property(user => user.BirthCertNo).IsRequired(false).HasMaxLength(50).HasComment("");
        builder.Property(user => user.BirthCertSerial).IsRequired(false).HasMaxLength(50).HasComment("");
        builder.Property(user => user.BirthPlace).IsRequired(false).HasMaxLength(150).HasComment("");
        builder.Property(user => user.BirthDate).IsRequired(false).HasComment("");
        builder.Property(user => user.DeathDate).IsRequired(false).HasComment("");
        builder.Property(user => user.Mobile).IsRequired(false).HasMaxLength(50).HasComment("");
        builder.Property(user => user.Tel).IsRequired(false).HasMaxLength(50).HasComment("");
        builder.Property(user => user.Email).IsRequired(false).HasMaxLength(150).HasComment("");
        builder.Property(user => user.GenderId).IsRequired(false).HasComment("");
        builder.Property(user => user.IsAlive).IsRequired(false).HasComment("");
        builder.Property(user => user.SabteAhvalApproved).IsRequired(false).HasComment("");
        builder.Property(user => user.IsDeleted).IsRequired(false).HasComment("");
        builder.Property(user => user.ImageId).IsRequired(false).HasComment("");
        builder.Property(user => user.CreatedDate).IsRequired(false).HasComment("");
        builder.Property(user => user.UpdatedDate).IsRequired(false).HasComment("");
        builder.Property(user => user.CreatedByGUID).IsRequired(false).HasComment("");
        builder.Property(user => user.UpdatedByGUID).IsRequired(false).HasComment("");
        builder.HasIndex(user => user.SSOUserId).IsUnique();



    }
}


//internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
//{
//    public void Configure(EntityTypeBuilder<User> builder)
//    {
//        builder.ToTable("Users");

//        builder.HasKey(x => x.Id);

//        builder.Property(x => x.Id)
//            .ValueGeneratedNever()
//            .HasMaxLength(450);

//        builder.Property(x => x.FirstName)
//            .HasMaxLength(150)
//            .IsRequired(false);

//        builder.Property(x => x.LastName)
//            .HasMaxLength(150)
//            .IsRequired(false);

//        builder.Property(x => x.Email)
//            .HasMaxLength(250)
//            .IsRequired();

//        builder.Property(x => x.NormalizedEmail)
//            .HasMaxLength(250)
//            .IsRequired();

//        builder.Property(x => x.EmailConfirmed)
//            .HasDefaultValue(true) // TODO: remove this
//            .IsRequired();

//        builder.Property(x => x.UserName)
//            .IsRequired()
//            .HasMaxLength(250);

//        builder.HasIndex(user => user.UserName).IsUnique();

//        builder.Property(x => x.NormalizedUserName)
//            .IsRequired()
//            .HasMaxLength(250);

//        builder.Property(x => x.PhoneNumber)
//            .HasMaxLength(14)
//            .IsRequired(false);

//        builder.Property(x => x.RegisterDate)
//            .IsRequired();

//        builder.Property(x => x.CompletelyRegistered)
//            .IsRequired();

//        builder.Property(x => x.IsBlocked)
//            .IsRequired();

//        builder.Property(x => x.Address)
//            .HasMaxLength(1000)
//            .IsRequired(false);

//        builder.Property(x => x.Gender)
//            .IsRequired(false);

//        builder.Property(x => x.BirthDate)
//            .IsRequired(false);

//        builder.Property(x => x.NationalCode)
//            .HasMaxLength(10)
//            .IsRequired(false);

//        builder.HasMany(x => x.Roles)
//            .WithOne(x => x.User)
//            .HasForeignKey(x => x.RoleId)
//            .OnDelete(DeleteBehavior.NoAction);
//    }
//}