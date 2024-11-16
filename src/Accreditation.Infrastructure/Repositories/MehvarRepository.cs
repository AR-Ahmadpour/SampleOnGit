using Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;
using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Mehvars.GetList;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Domain.Mehvars.Dtos;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SharedKernel;


namespace Accreditation.Infrastructure.Repositories;

internal sealed class MehvarRepository(AccreditationDbContext context) : IMehvarRepository
{
    public void Add(Mehvar mehvar)
    {
        context.Mehvars.Add(mehvar);
    }

    public void Delete(Mehvar mehvar)
    {
        context.Mehvars.Remove(mehvar);
    }

    public async Task<Mehvar?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Mehvars.FindAsync(guid, cancellationToken);
    }

    public async Task<bool> AnyAsync(Guid? guid, CancellationToken cancellationToken = default)
    {
        return await context.Mehvars.AnyAsync(x => x.GUID == guid, cancellationToken);
    }

    public async Task<bool> IsTitleUniqueAsync(Guid? guid, Guid etebarDorehGUID, string title,CancellationToken cancellationToken = default)
    {
        return !await context.Mehvars.AnyAsync(x => x.GUID != guid && x.EtebarDorehGUID == etebarDorehGUID && x.Title == title, cancellationToken);
    }

    //public Task<bool> IsTitleUniqueAsync(Guid? guid, Guid etebarDorehGUID, string title, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<bool> IsTitleUniqueAsync(Guid id, string title, Guid etebardorehguid, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Mehvar?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await context.Mehvars.Include(x => x.EtebarDoreh.OrgType).AsNoTracking().FirstOrDefaultAsync(u => u.GUID == guid, cancellationToken);
    }

    //public async Task<bool> IsTitleUniqueAsync(Guid guid, string title, CancellationToken cancellationToken = default)
    //{
    //    return !await context.Mehvars.AnyAsync(u => u.GUID != guid && u.Title == title, cancellationToken);
    //}

    public async Task<PagedList<GetAllByEtebarDorehDto>> GetAllByEtebarDorehIdAsync
        (Guid etebarDorehGUID, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = context.Mehvars
          .Where(x => x.EtebarDorehGUID == etebarDorehGUID)
          .Select(x => new GetAllByEtebarDorehDto
          {
              Guid = x.GUID,
              EtebarDorehTitle = x.EtebarDoreh.Title,
              OrgTypeTitle = x.EtebarDoreh.OrgType.Title ?? string.Empty,
              Title = x.Title,
              WeightedCoefficient = x.WeightedCoefficient,
              SortOrder = x.SortOrder,
              IsDeleted = x.IsDeleted
          })
          .AsNoTracking()
          .OrderBy(x => x.SortOrder)
          .AsQueryable();

        return await PagedList<GetAllByEtebarDorehDto>.Paginate(
           source: query,
           pageNumber: pageNumber,
           pageSize: pageSize,
           cancellationToken: cancellationToken);
    }

    public async Task<List<Guid>> GetSelectListAsync(Guid eteberDorehGuid)
    {
        return context.Mehvars
                      .AsNoTracking()
                      .Where(x => x.EtebarDorehGUID == eteberDorehGuid)
                      .Select(x => x.GUID).ToList();
    }


    public async Task<List<MehvarDto>> GetAllByEtebarDorehIdMehvarsAsync(Guid etebardorehGUID, CancellationToken cancellationToken = default)
    {
        return await context.Mehvars
                    .AsNoTracking()
                   .Where(x => x.EtebarDorehGUID == etebardorehGUID)
                   .Select(x => new MehvarDto
                   {
                       MehvarGuid = x.GUID,
                       MehvarName = x.Title,
                   })
                   .ToListAsync();
    }

    public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
    {

        {
            var breadCrumbs = new List<GetBreadCrumbDto>();


            var mehvarData = await context.Mehvars
                .Where(m => m.GUID == guid)
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

            return breadCrumbs;



        }
    }

    public async Task<List<GetListOfMehvarsByEtebarDorehIdDto>> GetAllMehvarByEtebarDorehIdAsync(Guid etebardoRehGUID, CancellationToken cancellationToken = default)
    {
        var query = await context.Mehvars
            .Where(x => x.EtebarDorehGUID == etebardoRehGUID && !x.IsDeleted)
            .Select(x => new GetListOfMehvarsByEtebarDorehIdDto
            {
                Guid = x.GUID,
                Title = x.Title,

            })
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync();

        return query;
    }


    public async Task<List<GetListMehvarsInArzyabiDakheliQueryDto>> GetListMehvarsInArzyabiDakheliAsync(GetListMehvarsInArzyabiDakheliQuery query, CancellationToken cancellationToken)
    {
        #region Old Temperory
        //return await context.Sanjehs
        //        .Where(s => !s.IsDeleted
        //        && s.EtebarDorehGUID == query.EtebardorehId
        //        && s.SanjehLevelId == query.SanjehLevelId)
        //        .GroupBy(s => new { s.Mehvar.GUID, s.Mehvar.Title, s.ZirMehvarGUID })
        //        .Select(g => new GetListMehvarsInArzyabiDakheliQueryDto
        //        {
        //            MehvarId = g.Key.GUID,
        //            ZirMehvarId = g.Key.ZirMehvarGUID,
        //            MehvarTitel = g.Key.Title,
        //            SanjehCount = g.Count(),
        //        })
        //        .ToListAsync();



        //    return await context.Sanjehs
        //        .Where(s => !s.IsDeleted
        //                    && s.EtebarDorehGUID == query.EtebardorehId
        //                    && s.SanjehLevelId == query.SanjehLevelId)
        //        .GroupJoin(
        //            context.AccreditationalInstanceAnswers,
        //            sanjeh => sanjeh.GUID,   // Key from Sanjehs
        //            answer => answer.SanjehGUID,  // Key from AccreditationalInstanceAnswers
        //            (sanjeh, answers) => new { Sanjeh = sanjeh, Answers = answers })  // Result selector
        //        .SelectMany(
        //            sa => sa.Answers.DefaultIfEmpty(),   // Flatten and handle cases where there are no answers
        //            (sa, answer) => new { sa.Sanjeh, Answer = answer })  // Result selector after flattening            
        //.GroupBy(
        //    sa => new { sa.Sanjeh.Mehvar.GUID, sa.Sanjeh.Mehvar.Title })  // Group by Mehvar GUID and Title only
        //.Select(g => new
        //{
        //    MehvarId = g.Key.GUID,
        //    MehvarTitel = g.Key.Title,
        //    SanjehCount = g.Select(sa => sa.Sanjeh.GUID).Distinct().Count(),  // Count unique Sanjehs
        //    ResultCount = g.Where(sa => sa.Answer != null && sa.Answer.Result != null) // Check More 1403-06-07
        //                   .Select(sa => sa.Answer.SanjehGUID).Distinct().Count()  // Count unique answers with non-null Results
        //})
        //.Select(g => new GetListMehvarsInArzyabiDakheliQueryDto
        //{
        //    MehvarId = g.MehvarId,
        //    MehvarTitel = g.MehvarTitel,
        //    SanjehCount = g.SanjehCount,
        //    ResultCount = g.ResultCount,
        //    FlagState = g.ResultCount == g.SanjehCount ? 0 : (g.ResultCount < g.SanjehCount ? 1 : 2)
        //})
        //.ToListAsync();
        #endregion 



        return await context.Sanjehs
        .Where(s => !s.IsDeleted
                    && s.EtebarDorehGUID == query.EtebardorehId
                    && s.SanjehLevelId == query.SanjehLevelId)
            .GroupBy(
                s => new { s.Mehvar.GUID, s.Mehvar.Title })  // Group by Mehvar GUID and Title only
            .Select(g => new
            {
                MehvarId = g.Key.GUID,
                MehvarTitel = g.Key.Title,
                SanjehCount = g.Count(),  // Count total Sanjeh for this group
                ResultCount = context.AccreditationalInstanceAnswers
                    .Where(answer => answer.Result != null && g.Select(s => s.GUID).Contains(answer.SanjehGUID))
                    .Select(answer => answer.SanjehGUID)
                    .Distinct()
                    .Count()  // Count unique answers with non-null Results for current Sanjehs in the group
            })
            .Select(g => new GetListMehvarsInArzyabiDakheliQueryDto
            {
                MehvarId = g.MehvarId,
                AccInstanceID= query.AccInstanceID,
                MehvarTitel = g.MehvarTitel,
                SanjehCount = g.SanjehCount,
                ResultCount = g.ResultCount,
                FlagState = g.ResultCount == g.SanjehCount ? 0 : (g.ResultCount < g.SanjehCount ? 1 : 2),
                EtebardorehId=query.EtebardorehId
            }).ToListAsync();










    }

   
}



