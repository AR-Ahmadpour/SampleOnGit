using Accreditation.Application.Fields.Add;
using Accreditation.Application.FileTables.Add;
using Accreditation.Application.FileTables.Get;
using Accreditation.Domain.FileTables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Persistence.FileTables
{
    public interface IFileTableRepository
    {
        void Add(FileTable filetable);
        void Delete(FileTable filetable ,CancellationToken cancellationToken);
        Task<GetFileTableByNameDto> GetByName(string Name, CancellationToken cancellationToken);
        Task<FileTable> FindByName(string Name, CancellationToken cancellationToken);
    }
}
