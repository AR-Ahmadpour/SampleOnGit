using Accreditation.Domain.Sanjehs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.Excel
{
    public interface IExcelRepository
    {
        Task ProcessExcelFileAsync(byte[] fileContent, CancellationToken cancellationToken);
        Task ProcessExcelMommayeziFileAsync(byte[] fileContent, CancellationToken cancellationToken);

    }
}
