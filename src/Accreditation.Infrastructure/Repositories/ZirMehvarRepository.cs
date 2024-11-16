using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Application.ZirMehvars.Dtos;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using Accreditation.Domain.ZirMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Accreditation.Infrastructure.Repositories;

internal class ZirMehvarRepository(AccreditationDbContext context) : IZirMehvarRepository
{
    public void Add(ZirMehvar zirMehvar)
    {
        context.ZirMehvars.Add(zirMehvar);
    }

    public async Task<bool> IsTitleUniqueAsync(Guid? guid, Guid mehvarGuid, string title, CancellationToken cancellationToken = default)
    {
        return !await context.ZirMehvars.AnyAsync(x => x.GUID != guid && x.MehvarGUID == mehvarGuid && x.Title == title, cancellationToken);
    }

    public async Task<bool> AnyAsync(Guid? guid, CancellationToken cancellationToken = default)
    {
        return await context.ZirMehvars.AnyAsync(x => x.GUID == guid, cancellationToken);
    }

    public void Delete(ZirMehvar zirMehvar)
    {
        context.ZirMehvars.Remove(zirMehvar);
    }

    public async Task<ZirMehvar?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.ZirMehvars.FindAsync(guid, cancellationToken);
    }

    public async Task<PagedList<GetListByMehvarIdAsyncDto>> GetListByMehvarIdAsync
        (Guid mehvarid, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = context.ZirMehvars
                    .Where(x => x.MehvarGUID == mehvarid)
                    .Select(x => new GetListByMehvarIdAsyncDto
                    {
                        Title = x.Title,
                        IsDeleted = x.IsDeleted,
                        SortOrder = x.SortOrder,
                        WeightedCoefficient = x.WeightedCoefficient,
                        GUID = x.GUID,
                    })
                    .AsNoTracking()
                    .OrderBy(x => x.SortOrder)
                    .AsQueryable();

        return await PagedList<GetListByMehvarIdAsyncDto>.Paginate(
          source: query,
          pageNumber: pageNumber,
          pageSize: pageSize,
          cancellationToken: cancellationToken);
    }

    public async Task<ZirMehvar?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.ZirMehvars.AsNoTracking().FirstOrDefaultAsync(u => u.GUID == guid, cancellationToken);
    }
    public async Task<List<ZirMehvar>> GetSelectListAsync(Guid etebarDorehGuid)
    {
        return context.ZirMehvars
                      .Include(x => x.Mehvar)
                      .AsNoTracking()
                      .Where(x => x.Mehvar.EtebarDorehGUID == etebarDorehGuid)
                      .ToList();
    }

    public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
    {
        var breadCrumbs = new List<GetBreadCrumbDto>();


        var zirmehvarData = await context.ZirMehvars
            .Where(z => z.GUID == guid)
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
                                        GuidType = "orgtype" 
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        return breadCrumbs;
    }


    public async Task<List<GetListZirMehvarsInArzyabiDakheliDto>> GetListZirMehvarsInArzyabiDakheliAsync(GetListZirMehvarsInArzyabiDakheliQuery query, CancellationToken cancellationToken)
    {
        #region Old Temperory
        //return await context.Sanjehs
        //    .Where(s => !s.IsDeleted 
        //    &&  s.MehvarGUID == query.MehvarsId
        //    && s.SanjehLevelId == query.SanjehLevelId)
        //    .GroupBy(s => new { s.ZirMehvar.GUID, s.ZirMehvar.Title  })
        //    .Select(g => new GetListZirMehvarsInArzyabiDakheliDto
        //    {
        //        ZirMehvarId = g.Key.GUID,
        //        //StandardId=g.Key.StandardGUID,
        //        MehvarId= query.MehvarsId ,//g.Key.MehvarGUID,
        //        ZirMehvarTitel = g.Key.Title,
        //        SanjehCount = g.Count(),
        //        ResultCount=
        //    })
        //    .ToListAsync();






        //return await context.Sanjehs
        //    .Where(s => !s.IsDeleted
        //                && s.MehvarGUID == query.MehvarsId
        //                && s.SanjehLevelId == query.SanjehLevelId)
        //    .GroupJoin(
        //        context.AccreditationalInstanceAnswers,
        //        sanjeh => sanjeh.GUID,   // Key from Sanjehs
        //        answer => answer.SanjehGUID,  // Key from AccreditationalInstanceAnswers
        //        (sanjeh, answers) => new { Sanjeh = sanjeh, Answers = answers })  // Result selector
        //    .SelectMany(
        //        sa => sa.Answers.DefaultIfEmpty(),   // Flatten and handle cases where there are no answers
        //        (sa, answer) => new { sa.Sanjeh, Answer = answer })  // Result selector after flattening            
        //    .GroupBy(
        //        sa => new { sa.Sanjeh.Mehvar.GUID, sa.Sanjeh.Mehvar.Title })  // Group by Mehvar GUID and Title only
        //    .Select(g => new
        //    {
        //        ZirMehvarId = g.Key.GUID,
        //        MehvarId = query.MehvarsId,
        //        ZirMehvarTitel = g.Key.Title,
        //        SanjehCount = g.Select(sa => sa.Sanjeh.GUID).Distinct().Count(),  // Count unique Sanjehs
        //        ResultCount = g.Where(sa => sa.Answer != null && sa.Answer.Result != null) // Check More 1403-06-07
        //                       .Select(sa => sa.Answer.SanjehGUID).Distinct().Count()  // Count unique answers with non-null Results
        //    })
        //    .Select(g => new GetListZirMehvarsInArzyabiDakheliDto
        //    {
        //        ZirMehvarId = g.ZirMehvarId,
        //        MehvarId = query.MehvarsId,
        //        ZirMehvarTitel = g.ZirMehvarTitel,
        //        SanjehCount = g.SanjehCount,
        //        ResultCount = g.ResultCount,
        //        FlagState = g.ResultCount == g.SanjehCount ? 0 : (g.ResultCount < g.SanjehCount ? 1 : 2)
        //    })
        //    .ToListAsy





        #endregion

        return await context.Sanjehs
               .Where(s => !s.IsDeleted
                && s.MehvarGUID == query.MehvarsId
                && s.SanjehLevelId == query.SanjehLevelId)
                   .GroupBy(
                       s => new { s.ZirMehvar.GUID, s.ZirMehvar.Title })  // Group by Mehvar GUID and Title only
                   .Select(g => new
                   {
                       ZirMehvarId = g.Key.GUID,
                       MehvarId = query.MehvarsId,
                       ZirMehvarTitel = g.Key.Title,
                       SanjehCount = g.Count(),  // Count total Sanjeh for this group
                       ResultCount = context.AccreditationalInstanceAnswers
                                .Where(answer => answer.Result != null && g.Select(s => s.GUID).Contains(answer.SanjehGUID))
                                .Select(answer => answer.SanjehGUID)
                                .Distinct()
                                .Count()  // Count unique answers with non-null Results for current Sanjehs in the group

                   })
                   .Select(g => new GetListZirMehvarsInArzyabiDakheliDto
                   {

                       ZirMehvarId = g.ZirMehvarId,
                       MehvarId = query.MehvarsId,
                       ZirMehvarTitel = g.ZirMehvarTitel,
                       SanjehCount = g.SanjehCount,
                       ResultCount = g.ResultCount,
                       AccInstanceID=query.AccInstanceID,
                       FlagState = g.ResultCount == g.SanjehCount ? 0 : (g.ResultCount < g.SanjehCount ? 1 : 2)                       
                   }).ToListAsync();



    }

}
