using Accreditation.Domain.FileTables.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Database.Configurations
{
    internal class FileTableConfiguration:IEntityTypeConfiguration<FileTable>
    {
        public void Configure(EntityTypeBuilder<FileTable> builder)
        {
            builder.ToTable("AccDocument");
            builder.HasKey(ft => ft.stream_id); 

            builder
            .Property(ft => ft.cached_file_size)
            .HasComputedColumnSql("calculated column SQL");

            builder
            .Property(ft => ft.file_type)
            .HasComputedColumnSql("calculated column SQL");
        }
    }
}
