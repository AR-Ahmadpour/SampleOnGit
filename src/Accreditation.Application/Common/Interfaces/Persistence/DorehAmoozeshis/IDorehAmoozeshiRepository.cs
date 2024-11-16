using Accreditation.Application.DorehAmoozeshis.GetList;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Domain.DorehAmoozeshis.Entities;
using Accreditation.Domain.Sanjehs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.DorehAmoozeshis
{
    public interface IDorehAmoozeshiRepository
    {
        Task<DorehAmoozeshi?> FindAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<List<GetListDorehAmoozeshiDto>> GetListDorehAmoozeshiAsync(CancellationToken cancellationToken = default);
    }
}
