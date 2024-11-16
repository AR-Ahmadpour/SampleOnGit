using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs.GetSelectList;
using Accreditation.Domain.EtebarDorehs.Dtos;
using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class EtebarDorehRepository(AccreditationDbContext context) : IEtebarDorehRepository
{
    public void Add(EtebarDoreh etebarDoreh)
    {
        context.EtebarDorehs.Add(etebarDoreh);
    }

    public void Delete(EtebarDoreh etebarDoreh)
    {
        context.EtebarDorehs.Remove(etebarDoreh);
    }

    public async Task<EtebarDoreh?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.FindAsync(guid, cancellationToken);
    }

    public async Task<EtebarDoreh?> FindCurrentEtebarDorehAsync(Guid OrgTypeGuid,CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.FirstOrDefaultAsync(x => x.IsCurrent == true && x.OrgTypeGUID == OrgTypeGuid);
    }

    public async Task<bool> AnyAsync(Guid? guid, CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.AnyAsync(x => x.GUID == guid, cancellationToken);
    }

    public async Task<bool> IsThereAlreadyAnActiveEtebarDoreAsync(Guid? guid, bool isCurrent, CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.AnyAsync(x => x.GUID != guid && x.IsCurrent == isCurrent, cancellationToken);
    }

    public async void FindByIdAsync(EtebarDoreh etebarDoreh, CancellationToken cancellationToken = default)
    {
        await context.EtebarDorehs.FindAsync(etebarDoreh, cancellationToken);
    }

    public async Task<EtebarDoreh?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.AsNoTracking().FirstOrDefaultAsync(u => u.GUID == guid, cancellationToken);
    }


    public async Task<bool> IsTitleUniqueAsync(Guid? guid, Guid OrgtypeGuid,string title, CancellationToken cancellationToken = default)
    {
        return !await context.EtebarDorehs.AnyAsync(u => u.GUID != guid && u.OrgTypeGUID == OrgtypeGuid && u.Title == title, cancellationToken);
    }

    public async Task<PagedList<GetListDto>> GetListAsync
        (int pageNumber, int pageSize, Guid? Orgtype,CancellationToken cancellationToken)
    {

        //Change By Ar.Ahmadpour Add CreatedByUser And  UpdatedByUser Relations
        //var query = context.EtebarDorehs
        //.Include(x => x.CreatedByUser)
        //.Include(x => x.UpdatedByUser)
        //.Include(x => x.OrgType)
        //.Select(x => new GetListDto
        //{
        //    Guid = x.GUID,
        //    Title = x.Title,
        //    OrgTypeTitle = x.OrgType.Title ?? string.Empty,
        //    CreationDate = x.CreationDate,
        //    CreatedByGUID = x.CreatedByGUID,
        //    CreatedByFullUserName = x.CreatedByUser != null ? x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName : "",
        //    StartDate = x.StartDate,
        //    EndDate = x.EndDate,
        //    UpdateDate = x.UpdateDate,
        //    UpdatedByGUID = x.UpdatedByGUID,
        //    UpdatedByFullUserName = x.UpdatedByUser != null ? x.UpdatedByUser.FirstName + " " + x.UpdatedByUser.LastName : "",
        //    SortOrder = x.SortOrder,
        //    IsCurrent = x.IsCurrent,
        //    IsDeleted = x.IsDeleted
        //})
        //.AsNoTracking()
        //.OrderBy(x => x.SortOrder)
        //.AsQueryable();



        //return await PagedList<GetListDto>.Paginate(
        //    source: query,
        //    pageNumber: pageNumber,
        //    pageSize: pageSize,
        //    cancellationToken: cancellationToken);



        var query = context.EtebarDorehs
        .Include(x => x.CreatedByUser)
        .Include(x => x.UpdatedByUser)
        .Include(x => x.OrgType)
        .AsNoTracking()
        .AsQueryable();

        // Apply filtering based on OrgTypeGUID if provided
        if (Orgtype.HasValue)
        {
            query = query.Where(x => x.OrgTypeGUID == Orgtype.Value);
        }

        // Select into GetListDto after applying the filter
        var result = query.Select(x => new GetListDto
        {
            Guid = x.GUID,
            Title = x.Title,
            OrgTypeTitle = x.OrgType.Title ?? string.Empty,
            CreationDate = x.CreationDate,
            CreatedByGUID = x.CreatedByGUID,
            CreatedByFullUserName = x.CreatedByUser != null ? x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName : "",
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            UpdateDate = x.UpdateDate,
            UpdatedByGUID = x.UpdatedByGUID,
            UpdatedByFullUserName = x.UpdatedByUser != null ? x.UpdatedByUser.FirstName + " " + x.UpdatedByUser.LastName : "",
            SortOrder = x.SortOrder,
            IsCurrent = x.IsCurrent,
            IsDeleted = x.IsDeleted
        })
        .OrderBy(x => x.SortOrder)
        .AsQueryable();

        return await PagedList<GetListDto>.Paginate(
            source: result,
            pageNumber: pageNumber,
            pageSize: pageSize,
            cancellationToken: cancellationToken);
    }

    public async Task<List<GetEtebarDoreSelectListDto>> GetSelectListByOrgTypeIdAsync(Guid orgTypeGUID, CancellationToken cancellationToken = default)
    {
        return await context.EtebarDorehs.Where(x => x.OrgTypeGUID == orgTypeGUID & !x.IsDeleted)
            .Select(x => new GetEtebarDoreSelectListDto
            {
                Guid = x.GUID,
                Title = x.Title,
                IsCurrent = x.IsCurrent
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
    {
        var breadCrumbs = new List<GetBreadCrumbDto>();


        var etebardorehData = await context.EtebarDorehs
            .Where(e => e.GUID == guid)
            .Select(e => new
            {
                Guid = e.GUID,
                Title = e.Title,
                OrgTypeGUID = e.OrgTypeGUID
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (etebardorehData != null)
        {
            breadCrumbs.Add(new GetBreadCrumbDto
            {
                Guid = etebardorehData.Guid,
                Title = etebardorehData.Title,
                GuidType = "etebardoreh"
            });


            if (etebardorehData.OrgTypeGUID != null)
            {
                var orgTypeData = await context.OrgType
                    .Where(o => o.GUID == etebardorehData.OrgTypeGUID)
                    .Select(o => new
                    {
                        Guid = o.GUID,
                        Title = o.Title
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (orgTypeData != null)
                {
                    breadCrumbs.Add(new GetBreadCrumbDto
                    {
                        Guid = orgTypeData.Guid,
                        Title = orgTypeData.Title,
                        GuidType = "orgtype"
                    });
                }
            }
        }

        return breadCrumbs;
    }

    public Task<bool> IsTitleUniqueAsyncAdd(Guid OrgtypeGuid, string title, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}


