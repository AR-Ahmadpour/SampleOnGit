using Accreditation.Domain.AccreditationInstanceAnswers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accreditation.Infrastructure.Database.Configurations;

internal sealed class AccreditationalInstanceAnswerConfiguration : IEntityTypeConfiguration<AccreditationalInstanceAnswer>
{
    public void Configure(EntityTypeBuilder<AccreditationalInstanceAnswer> builder)
    {
        builder.ToTable("AccreditationInstanceAnswer");

        builder.HasKey(x => x.GUID);

        builder.Property(x => x.DateTime).HasColumnType("datetime");
        builder.Property(x => x.AuditDateTime).HasColumnType("datetime");
        builder.Property(x => x.SanjehGUID);
        builder.Property(x => x.UserGUID).HasComment("کاربر پاسخگو به سوالات");
        builder.Property(x => x.AuditGUID);
        builder.Property(x => x.Result);
        builder.Property(x => x.Universityopinion);
        builder.Property(x => x.UniversityopinionText).HasMaxLength(1000);
        builder.Property(x => x.AuditResult);
        builder.Property(x => x.AccreditationInstanceGUID);
        builder.Property(x => x.AnswerText);

        builder.HasOne(x => x.AccreditationalInstance)
                 .WithMany()
                 .HasForeignKey(x => x.AccreditationInstanceGUID)
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Sanjeh)
                .WithMany()
                .HasForeignKey(x => x.SanjehGUID)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
        .WithMany()
        .HasForeignKey(x => x.UserGUID)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
