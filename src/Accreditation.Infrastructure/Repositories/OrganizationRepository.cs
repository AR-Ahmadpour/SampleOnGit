using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Organization.GetList;
using Accreditation.Application.Organizations.GetById;
using Accreditation.Domain.Organizations.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal class OrganizationRepository(AccreditationDbContext context) : IOrganizationRepository
{
    public async Task<PagedList<GetListOrganizationDto>> GetSelectListOrganizationAsync(
    Guid? orgTypeGuid,
    Guid? orgGerayeshGuid,
    int? ostanId,
    int? shahrestanId,
    int? bakhshLocationId,
    int? shahrId,
    int? universityId,
    string? OrganizationName,
    int pageNumber, int pageSize,
    CancellationToken cancellationToken = default)
    {

        var query = context.Organizations
            .Include(x => x.Shahrestan)
            .Include(x => x.University)
            .Include(x => x.OrgGerayesh)
            .ThenInclude(x => x.OrgType) // Change By Ahmadpour 1403-06-18 Beacuse Fault Result
             //.Include(x => x.OrgType)
            .Where(x => !x.IsTatilDaem)
            .AsNoTracking()
            .AsQueryable();

        //var query =
        //(

        //  from org in context.Organizations.AsNoTracking() 

        //  join shahrestan in context.Shahrestans on org.ShahrestanId equals shahrestan.Id 
        //  into shahrestanGroup from shahrestan in shahrestanGroup.DefaultIfEmpty()

        //  join university in context.Universities on org.UniversityId equals university.Id 
        //  into universityGroup from university in universityGroup.DefaultIfEmpty()

        //  join orgGerayesh in context.OrgGerayeshes on org.OrgGerayeshGUID equals orgGerayesh.GUID 
        //  into orgGerayeshGroup from orgGerayesh in orgGerayeshGroup.DefaultIfEmpty()

        //  join orgType in context.OrgTypes on org.OrgTypeGUID equals orgType.GUID 
        //  into orgTypeGroup from orgType in orgTypeGroup.DefaultIfEmpty() 


        //  where !org.IsTatilDaem
        // select org

        //).AsNoTracking().AsQueryable();


        query = CheckFilter(orgTypeGuid, orgGerayeshGuid,
                            ostanId, shahrestanId,
                            bakhshLocationId, shahrId,
                            universityId, OrganizationName,
                            query);

        var queryDto = query
             .Select(x => new GetListOrganizationDto
             {
                 Guid = x.GUID,
                 IsTatilDaem = x.IsTatilDaem,
                 GerayeshName = x.OrgGerayesh.Title,
                 SharestanName = x.Shahrestan.Title,
                 UniversityName = x.University.Title,
                 OrgTypeName = x.OrgGerayesh.OrgType.Title,
                 Name = x.Name

             }).AsNoTracking()
               .AsQueryable();

        return await PagedList<GetListOrganizationDto>.Paginate(
             source: queryDto,
             pageNumber: pageNumber,
             pageSize: pageSize,
             cancellationToken: cancellationToken);
    }

    private static IQueryable<Organization> CheckFilter(Guid? orgTypeGuid,
                                                        Guid? orgGerayeshGuid,
                                                        int? ostanId,
                                                        int? shahrestanId,
                                                        int? bakhshLocationId,
                                                        int? shahrId,
                                                        int? universityId,
                                                        string? OrganizationName,
                                                        IQueryable<Organization> query)
    {
        if (orgTypeGuid.HasValue)
        {
            query = query.Where(x => x.OrgGerayesh.OrgType.GUID == orgTypeGuid.Value);
        }
        if (orgGerayeshGuid.HasValue)
        {
            query = query.Where(x => x.OrgGerayeshGUID == orgGerayeshGuid.Value);
        }

        if (ostanId.HasValue)
        {
            query = query.Where(x => x.OstanId == ostanId.Value);
        }

        if (shahrestanId.HasValue)
        {
            query = query.Where(x => x.ShahrestanId == shahrestanId.Value);
        }

        if (bakhshLocationId.HasValue)
        {
            query = query.Where(x => x.BakhshLocationId == bakhshLocationId.Value);
        }

        if (shahrId.HasValue)
        {
            query = query.Where(x => x.ShahrId == shahrId.Value);
        }
        if (!string.IsNullOrEmpty(OrganizationName))
        {
            query = query.Where(x => x.Name.Contains(OrganizationName));
        }

        if (universityId.HasValue)
        {
            query = query.Where(x => x.UniversityId == universityId);
        }

        return query;
    }


    public async Task<bool> IsExistAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Organizations.AnyAsync(x => x.GUID == guid);
    }

    public async Task<OrganizationDto?> GetByIdAsync(Guid GUID, CancellationToken cancellationToken = default)
    {
        var query = context.Organizations
                           .Include(x => x.Shahrestan)
                           .Include(x => x.University)
                           .Include(x => x.OrgGerayesh)
                           .ThenInclude(x => x.OrgType)
                           .Where(x => !x.IsTatilDaem)
                           .AsNoTracking()
                           .AsQueryable();


        var queryDto = await query
             .Select(organization => new OrganizationDto
             {
                 OrganizationGuid = organization.GUID,
                 OrganizationTitle = organization.Name,
                 OrgTypeTitle = organization.OrgGerayesh.OrgType.Title,
                 University = organization.University.Title,
                 OrgTypeGuid = organization.OrgGerayesh.OrgType.GUID,

             }).AsNoTracking()
               .ToListAsync();

        return  queryDto.FirstOrDefault(x => x.OrganizationGuid == GUID);
    }

    public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
    {
        var breadCrumbs = new List<GetBreadCrumbDto>();

        var orgTypeData = await context.OrgType
            .Where(o => o.GUID == guid)
            .Select(o => new GetBreadCrumbDto
            {
                Guid = o.GUID,
                Title = o.Title,
                GuidType = "orgtype"
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (orgTypeData != null)
        {
            breadCrumbs.Add(orgTypeData);
        }

        return breadCrumbs;
    }
}

