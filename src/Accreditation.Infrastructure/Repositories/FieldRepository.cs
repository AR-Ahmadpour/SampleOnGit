using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Fields.GetById;
using Accreditation.Application.Fields.GetFilterdList;
using Accreditation.Application.Fields.GetList;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.ZirMehvars.Dtos;
using Accreditation.Domain.Fields.Entities;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.SanjehFields.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;
internal sealed class FieldRepository(AccreditationDbContext context) : IFieldRepository
{
    public void Add(Field field)
    {
        context.Add(field);
    }

    public async Task<List<GetAllFilteredFieldQueryDto>> GetAllFilteredByEtebarDorehGuidAsync(Guid etebarDorehGuid, List<Guid> EvaluationArzyabsFields, int instanceTypeId)
    {
        var result = context.Fields.Where(x => x.EtebarDorehGUID == etebarDorehGuid).ToList();
        var filteredFields = result
                    .Where(x => x.InstanceTypeIds.Split(',')
                    .Contains(instanceTypeId.ToString()))
                    .ToList();

        var fieldsResults = filteredFields.Select(filterField => new GetAllFilteredFieldQueryDto
        {
            EtebarDorehGuid = filterField.EtebarDorehGUID,
            Guid = filterField.GUID,
            Name = filterField.Title,
            IsUsed = EvaluationArzyabsFields.Contains(filterField.GUID)
        }).ToList();

        var excludedFields = filteredFields.Select(x => x.GUID).Except(EvaluationArzyabsFields).ToList();

        return fieldsResults;
    }

    public async Task<List<GetAllFieldQueryDto>> GetAllByEtebarDorehGuidAsync(Guid etebarDorehGuid, int instanceTypeId)
    {
        var query = await context.Fields
                             .Where(x => x.EtebarDorehGUID == etebarDorehGuid)
                             .AsNoTracking()
                             .ToListAsync();

        var result = query
                     .Where(x => x.InstanceTypeIds.Split(',')
                     .Contains(instanceTypeId.ToString()))
                     .ToList();

        return result
            .Select(x => new GetAllFieldQueryDto
            {
                EtebarDorehGuid = x.EtebarDorehGUID,
                Guid = x.GUID,
                Name = x.Title
            }).ToList();
    }

    public async Task<Field?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Fields.FindAsync(guid, cancellationToken);
    }

    public async Task<Mehvar?> GetMehvarByIdAsync(Guid mehvarId)
    {
        return await context.Mehvars
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.GUID == mehvarId);
    }

    public async Task<List<ZirMehvar>> GetZirmehvarsByMehvarGuidAsync(Guid mehvarGuid)
    {
        return await context.ZirMehvars
                .Where(z => z.MehvarGUID == mehvarGuid)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<List<Standard>> GetStandardsByZirmehvarIdAsync(Guid zirmehvarId)
    {
        return await context.Standards
                .Where(s => s.ZirMehvarGUID == zirmehvarId)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<List<Sanjeh>> GetSanjehsByStandardIdAsync(Guid standardId)
    {
        return await context.Sanjehs
                .Where(s => s.StandardGUID == standardId)
                .AsNoTracking()
                .ToListAsync();
    }

    public  Task<PagedList<GetListByEtebarDorehIdDto>> GetListByEtebarDorehIdAsync(Guid etebarDorehId,
        int pageNumber,int pageSize, CancellationToken cancellationToken = default)
    {
        var query = context.Fields
                .Where(x => x.EtebarDorehGUID == etebarDorehId)
                .Select(x => new GetListByEtebarDorehIdDto
                {
                    Title = x.Title,
                    TitleCode = x.TitleCode,
                    InstanceTypeIds = x.InstanceTypeIds,
                    Guid = x.GUID

                })
                .AsNoTracking()
                .AsQueryable();


        return PagedList<GetListByEtebarDorehIdDto>.Paginate(
             source: query,
             pageNumber: pageNumber,
             pageSize: pageSize,
             cancellationToken: cancellationToken);
    }

    public Task<List<SanjehField>> GetByFieldGuidAsync(Guid fieldGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}