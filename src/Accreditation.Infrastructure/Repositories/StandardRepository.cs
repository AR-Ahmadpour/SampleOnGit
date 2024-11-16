using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli;
using Accreditation.Domain.Standards.Dtos;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class StandardRepository(AccreditationDbContext context) : IStandardRepository
{
    public void Add(Standard standard)
    {
        context.Standards.Add(standard);
    }

    public void Delete(Standard standard)
    {
        context.Standards.Remove(standard);
    }
    public async Task<bool> AnyAsync(Guid? guid, CancellationToken cancellationToken = default)
    {
        return await context.Standards.AnyAsync(x => x.GUID == guid, cancellationToken);
    }

    public async Task<bool> IsTitleUniqueAsync(Guid? guid, Guid zirMehvarGuid, string title, CancellationToken cancellationToken = default)
    {
        return !await context.Standards.AnyAsync(x => x.GUID != guid && x.ZirMehvarGUID == zirMehvarGuid && x.Title == title, cancellationToken);
    }

    public async Task<bool> IsCodeUnique(Guid? guid, Guid zirMehvarGuid, string code, CancellationToken cancellationToken = default)
    {
        return !await context.Standards.AnyAsync(x => x.GUID != guid && x.ZirMehvarGUID == zirMehvarGuid && x.Code == code, cancellationToken);
    }

    public async Task<Standard?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Standards.FindAsync(guid, cancellationToken);
    }

    public async Task<Standard?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Standards.AsNoTracking().FirstOrDefaultAsync(u => u.GUID == guid, cancellationToken);
    }

    public async Task<PagedList<GetAllByZirMehvarIdAsyncDto>> GetListByZirMehvarIdAsync(Guid zirMehvarGUID, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = context.Standards
             .Where(x => x.ZirMehvarGUID == zirMehvarGUID)
             .Select(x => new GetAllByZirMehvarIdAsyncDto
             {
                 GUID = x.GUID,
                 Title = x.Title,
                 Code = x.Code,
                 SortOrder = x.SortOrder,
                 WeightedCoefficient = x.WeightedCoefficient,
                 IsDeleted = x.IsDeleted

             })
             .AsNoTracking()
             .OrderBy(x => x.SortOrder)
             .AsQueryable();

        return await PagedList<GetAllByZirMehvarIdAsyncDto>.Paginate(
                 source: query,
                 pageNumber: pageNumber,
                 pageSize: pageSize,
                 cancellationToken: cancellationToken);

    }

    public async Task<List<Standard>> GetSelectListAsync(Guid etebarDorehGuid)
    {
        var standards = new List<Standard>();

        var zirmehvarGuids = await context.ZirMehvars
                                          .Include(x => x.Mehvar)
                                          .Where(x => x.Mehvar != null && x.Mehvar.EtebarDorehGUID == etebarDorehGuid)
                                          .Select(x => x.GUID)
                                          .ToListAsync();


        foreach (var item in zirmehvarGuids)
        {
            standards.AddRange(await context.Standards
                       .Where(x => x.ZirMehvarGUID == item)
                       .AsNoTracking()
                       .ToListAsync());
        }


        return standards;
    }

    public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
    {
        var breadCrumbs = new List<GetBreadCrumbDto>();


        var standardData = await context.Standards
            .Where(s => s.GUID == guid)
            .Select(s => new
            {
                Guid = s.GUID,
                Title = s.Title,
                ZirmehvarGUID = s.ZirMehvarGUID
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (standardData != null)
        {
            breadCrumbs.Add(new GetBreadCrumbDto
            {
                Guid = standardData.Guid,
                Title = standardData.Title,
                GuidType = "standard"
            });

            if (standardData.ZirmehvarGUID != null)
            {
                var zirmehvarData = await context.ZirMehvars
                    .Where(z => z.GUID == standardData.ZirmehvarGUID)
                    .Select(z => new
                    {
                        Guid = z.GUID,
                        Title = z.Title,
                        MehvarGUID = z.MehvarGUID
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (zirmehvarData != null)
                {
                    breadCrumbs.Add(new GetBreadCrumbDto
                    {
                        Guid = zirmehvarData.Guid,
                        Title = zirmehvarData.Title,
                        GuidType = "zirmehvar"
                    });

                    if (zirmehvarData.MehvarGUID != null)
                    {
                        var mehvarData = await context.Mehvars
                            .Where(m => m.GUID == zirmehvarData.MehvarGUID)
                            .Select(m => new
                            {
                                Guid = m.GUID,
                                Title = m.Title,
                                EtebarDorehGUID = m.EtebarDorehGUID
                            })
                            .FirstOrDefaultAsync(cancellationToken);

                        if (mehvarData != null)
                        {
                            breadCrumbs.Add(new GetBreadCrumbDto
                            {
                                Guid = mehvarData.Guid,
                                Title = mehvarData.Title,
                                GuidType = "mehvar"
                            });

                            if (mehvarData.EtebarDorehGUID != null)
                            {
                                var etebardorehData = await context.EtebarDorehs
                                    .Where(e => e.GUID == mehvarData.EtebarDorehGUID)
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
                                                GuidType = "orgtype" // Hardcoded value
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return breadCrumbs;
    }

    public async Task<List<GetListStandardsInArzyabiDakheliDto>> GetListZirMehvarsInArzyabiDakheliAsync(GetListStandardsInArzyabiDakheliQuery query, CancellationToken cancellationToken)
    {
        #region Old Temperory
        //return await context.Sanjehs
        //.Where(s => !s.IsDeleted 
        //&& s.StandardGUID==query.ZirMehvarGUID
        //&& s.SanjehLevelId==query.SanjehLevelId)
        //.GroupBy(s => new { s.Standard.GUID, s.Standard.Title, s.ZirMehvarGUID })
        //.Select(g => new GetListStandardsInArzyabiDakheliDto
        //{
        //    StandardId = g.Key.GUID,
        //    ParentZirMehvarId=g.Key.ZirMehvarGUID,
        //    StandardTitel = g.Key.Title,
        //    SanjehCount = g.Count()
        //})
        //.ToListAsync();
        //}
        #endregion

        return await context.Sanjehs
       .Where(s => !s.IsDeleted
        && s.ZirMehvarGUID == query.ZirMehvarGUID
        && s.SanjehLevelId == query.SanjehLevelId)
           .GroupBy(
               s => new { s.Standard.GUID, s.Standard.Title })  // Group by Mehvar GUID and Title only
           .Select(g => new
           {
               StandardId = g.Key.GUID,
               ParentZirMehvarId = query.ZirMehvarGUID,
               StandardTitel = g.Key.Title,
               SanjehCount = g.Count(),  // Count total Sanjeh for this group
               ResultCount = context.AccreditationalInstanceAnswers
                        .Where(answer => answer.Result != null && g.Select(s => s.GUID).Contains(answer.SanjehGUID))
                        .Select(answer => answer.SanjehGUID)
                        .Distinct()
                        .Count()  // Count unique answers with non-null Results for current Sanjehs in the group

           })
           .Select(g => new GetListStandardsInArzyabiDakheliDto
           {

               StandardId = g.StandardId,
               ParentZirMehvarId = g.ParentZirMehvarId,
               AccInstanceID=query.AccInstanceID,
               StandardTitel = g.StandardTitel,
               SanjehCount = g.SanjehCount,
               ResultCount = g.ResultCount,
               FlagState = g.ResultCount == g.SanjehCount ? 0 : (g.ResultCount < g.SanjehCount ? 1 : 2),
               EtebardorehId=query.EtebardorehId
           }).ToListAsync();
    }


 }
