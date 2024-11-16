using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Organization.GetList;
using Accreditation.Application.Organizations.GetById;

namespace Accreditation.Application.Common.Interfaces.Persistence.Organization;

public interface IOrganizationRepository
{
    Task<PagedList<GetListOrganizationDto>> GetSelectListOrganizationAsync(Guid? orgTypeGuid, Guid? orgGerayeshGuid,
                                                                  int? ostanId, int? shahrestanId,
                                                                  int? bakhshLocationId, int? shahrId,
                                                                  int? univaersityId, string? OrganizationName,
                                                                  int pageNumber, int pageSize,
                                                                  CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(Guid guid, CancellationToken cancellationToken = default);
    Task<OrganizationDto> GetByIdAsync(Guid GUID, CancellationToken cancellationToken = default);

    Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);
}
