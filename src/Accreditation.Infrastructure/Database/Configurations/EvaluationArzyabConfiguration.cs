using Accreditation.Domain.EvaluationArzyabs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class EvaluationArzyabConfiguration : IEntityTypeConfiguration<EvaluationArzyab>
{
    public void Configure(EntityTypeBuilder<EvaluationArzyab> builder)
    {
        builder.ToTable("EvaluationArzyab");

        builder.HasKey(x => x.GUID);
        builder.Property(x => x.AccreditationInstanceGUID);
        builder.Property(x => x.CreateUserGUID);
        builder.Property(x => x.ArzyabUserGUID);
        builder.Property(x => x.ArzyabRoleId);
        builder.Property(x => x.CreateDate);


        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.ArzyabUserGUID)
            .OnDelete(DeleteBehavior.NoAction);

        //builder
        //    .HasOne(x => x.AccreditationInstance)
        //    .WithMany()
        //    .HasForeignKey(x => x.AccreditationInstanceGUID)
        //    .OnDelete(DeleteBehavior.NoAction);

        builder
              .HasMany(e => e.EvaluationArzyabFields)
              .WithOne(f => f.EvaluationArzyab)
              .HasForeignKey(e => e.EvaluationArzyabGUID)
              .OnDelete(DeleteBehavior.NoAction);
    }
}

internal sealed class EvaluationArzyabFieldConfiguration : IEntityTypeConfiguration<EvaluationArzyabField>
{
    public void Configure(EntityTypeBuilder<EvaluationArzyabField> builder)
    {
        builder.ToTable("EvaluationArzyabField");

        builder.HasKey(x => x.GUID);
        builder.Property(x => x.EvaluationArzyabGUID);
        builder.Property(x => x.FieldGuid);

        builder
            .HasOne(x => x.Field)
            .WithMany()
            .HasForeignKey(x => x.FieldGuid)
            .OnDelete(DeleteBehavior.NoAction);

    }
}