using Accreditation.Domain.AccreditationInstances.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccreditationInstanceConfiguration : IEntityTypeConfiguration<AccreditationInstance>
{
    public void Configure(EntityTypeBuilder<AccreditationInstance> builder)
    {

        builder.ToTable("AccreditationInstance", tb => tb.HasTrigger("AccreditationInstance_Insert"));
        
        builder.HasKey(x => x.GUID);
        builder.Property(x => x.OrganizationGUID);
        builder.Property(x => x.AccreditationInstanceStatusTypeId);
        builder.Property(x => x.MasterGUID);
        builder.Property(x => x.EtebarDorehGUID);
        builder.Property(x => x.CreateByUserGUID);
        builder.Property(x => x.IsFinal);
        builder.Property(x => x.IsLast);
        builder.Property(x => x.LevelOneScore);
        builder.Property(x => x.LevelOnePossibleScore);
        builder.Property(x => x.LevelTwoScore);
        builder.Property(x => x.CreateDateTime).HasColumnType("datetime2");
        builder.Property(x => x.UpdateDateTime).HasColumnType("datetime2");
        builder.Property(x => x.SubmitDate).HasColumnType("datetime2");
        builder.Property(x => x.CreateByUserGUID);
        builder.Property(x => x.UpdateDateTime);
        builder.Property(x => x.FromDate).HasColumnType("date");
        builder.Property(x => x.ToDate).HasColumnType("date");
        builder.Property(x => x.IsDeleted);
        builder.Property(x => x.IsLocked);

        builder.HasOne(x => x.EtebarDoreh)
         .WithMany()
         .HasForeignKey(x => x.EtebarDorehGUID)
         .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.CreatedUser)
       .WithMany()
       .HasForeignKey(x => x.CreateByUserGUID)
       //.HasForeignKey(x =>x.UpdateByUserGUID)
       .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.EtebarDoreh)
       .WithMany()
       .HasForeignKey(x => x.EtebarDorehGUID)
       .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.InstanceType)
         .WithMany()
         .HasForeignKey(x => x.InstanceTypeId)
         .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey(x => x.OrganizationGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.AccreditationInstanceStatusType)
       .WithMany()
       .HasForeignKey(x => x.AccreditationInstanceStatusTypeId)
       .OnDelete(DeleteBehavior.NoAction);

        // Self-referencing relationship for MasterGUID
        builder.HasOne(x => x.MasterAccreditationInstance)
            .WithMany()
            .HasForeignKey(x => x.MasterGUID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
