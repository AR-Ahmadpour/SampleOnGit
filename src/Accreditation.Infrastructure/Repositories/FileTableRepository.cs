using Accreditation.Application.Common.Interfaces.Persistence.FileTables;
using Accreditation.Application.FileTables.Add;
using Accreditation.Application.FileTables.Get;
using Accreditation.Domain.FileTables.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class FileTableRepository(AccreditationDbContext context) :IFileTableRepository
    {
        public  void Add(FileTable filetable)
        {
            context.FileTables.Add(filetable);            
        }

        public async void Delete(FileTable filetable, CancellationToken cancellationToken)
        {
            context.FileTables.Remove(filetable);
        }

        public async Task<GetFileTableByNameDto?> GetByName(string Name, CancellationToken cancellationToken)
        {
            return await context.FileTables
                .Where(file => file.name== Name)
                .Select( _ => new GetFileTableByNameDto
                    {
                    Name=_.name ,
                    stream_id=_.stream_id,
                    file=_.file_stream,
                    filetype= _.file_type,
                    Size=_.cached_file_size
                }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<FileTable?> FindByName(string Name, CancellationToken cancellationToken)
        {
            return await context.FileTables.Where(file => file.name == Name).FirstOrDefaultAsync();
        }
    }
}
