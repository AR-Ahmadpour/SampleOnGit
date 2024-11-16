using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs.GetSelectList;
using Accreditation.Domain.EtebarDorehs.Dtos;
using Accreditation.Domain.EtebarDorehs.Entities;
using SharedKernel;

namespace Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;

public interface IEtebarDorehRepository
{
    void Add(EtebarDoreh etebarDoreh);

    void Delete(EtebarDoreh etebarDoreh);

    Task<EtebarDoreh?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EtebarDoreh?> FindCurrentEtebarDorehAsync(Guid OrgTypeGuid,CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Guid? id, CancellationToken cancellationToken = default);

    Task<bool> IsThereAlreadyAnActiveEtebarDoreAsync(Guid? id, bool isCurrent, CancellationToken cancellationToken = default);

    void FindByIdAsync(EtebarDoreh etebarDoreh, CancellationToken cancellationToken = default);

    Task<EtebarDoreh?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);

    Task<List<GetEtebarDoreSelectListDto>> GetSelectListByOrgTypeIdAsync(Guid orgTypeGUID, CancellationToken cancellationToken = default);

    Task<bool> IsTitleUniqueAsync(Guid? id,Guid OrgtypeGuid, string title, CancellationToken cancellationToken = default);
    

    Task<PagedList<GetListDto>> GetListAsync(int pageNumber, int pageSize, Guid? Orgtype,CancellationToken cancellationToken);
}


