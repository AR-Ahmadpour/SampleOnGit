using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Domain.FileTables.Entities
{
    public sealed class FileTable
    {
        public Guid stream_id { get; set; } = Guid.NewGuid(); // Primary key, set by default
        public string? name { get; set; }
        public byte[]? file_stream { get; set; }
        public string? file_type { get; set; }
        public long cached_file_size { get; set; }
        public DateTimeOffset creation_time { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset last_write_time { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset last_access_time { get; set; } = DateTimeOffset.UtcNow;
        public bool is_directory { get; set; } = false;
        public bool is_offline { get; set; } = false;
        public bool is_hidden { get; set; } = false;
        public bool is_readonly { get; set; } = false;
        public bool is_archive { get; set; } = false;
        public bool is_system { get; set; } = false;
        public bool is_temporary { get; set; } = false;

        // Default constructor
        public FileTable() { }

        // Parameterized constructor
        private FileTable(string? name, byte[]? file_stream)
        {
            this.name = name;
            this.file_stream = file_stream;

        }

        // Factory method to create a new FileTable instance
        public static FileTable Create(string? name, byte[]? file_stream)
        {
            return new FileTable(name, file_stream);
        }
    }
}
