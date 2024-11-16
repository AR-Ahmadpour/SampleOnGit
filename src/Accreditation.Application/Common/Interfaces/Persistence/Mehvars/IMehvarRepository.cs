using Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;
using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Mehvars.GetList;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Domain.Mehvars.Dtos;
using Accreditation.Domain.Mehvars.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
public interface IMehvarRepository
{
    void Add(Mehvar mehvar);

    void Delete(Mehvar mehvar);

    Task<Mehvar?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Guid? id, CancellationToken cancellationToken = default);

    Task<bool> IsTitleUniqueAsync(Guid? guid, Guid etebarDorehGUID, string title, CancellationToken cancellationToken = default);

    Task<List<Guid>> GetSelectListAsync(Guid eteberDorehGuid);

    Task<Mehvar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    //Task<bool> IsTitleUniqueAsync(Guid id, string title,Guid etebardorehguid, CancellationToken cancellationToken = default);

    Task<PagedList<GetAllByEtebarDorehDto>> GetAllByEtebarDorehIdAsync(
       Guid etebardoRehGUID, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<List<GetListOfMehvarsByEtebarDorehIdDto>> GetAllMehvarByEtebarDorehIdAsync(Guid etebardoRehGUID, CancellationToken cancellationToken = default);

    Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);

    Task<List<MehvarDto>> GetAllByEtebarDorehIdMehvarsAsync(Guid etebardorehGUID, CancellationToken cancellationToken = default);
    Task<List<GetListMehvarsInArzyabiDakheliQueryDto>> GetListMehvarsInArzyabiDakheliAsync(GetListMehvarsInArzyabiDakheliQuery query, CancellationToken cancellationToken);
}

