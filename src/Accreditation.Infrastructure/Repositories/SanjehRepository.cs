
using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.Sanjehs.GetSanjehListInArzyabiDakheli;
using Accreditation.Application.Sanjehs.GetSanjehValidResault;
using Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli;
using Accreditation.Domain.AccreditationInstanceAnswers.Entities;
using Accreditation.Domain.EtebarDorehs.Dtos;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.ComponentModel;
using System.Drawing.Printing;

namespace Accreditation.Infrastructure.Repositories
{
    internal class SanjehRepository(AccreditationDbContext context) : ISanjehRepository
    {
        public void Add(Sanjeh sanjeh)
        {
            context.Sanjehs.Add(sanjeh);
        }



        public async Task<Sanjeh?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await context.Sanjehs.FindAsync(guid, cancellationToken);
        }

        public async Task<List<Sanjeh>> GetByEtebarDorehGuidAsync(Guid etebarDorehGuid, CancellationToken cancellationToken = default)
        {
            return await context.Sanjehs
                .Where(s => s.EtebarDorehGUID == etebarDorehGuid)
                .OrderBy(x => x.SortOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<Sanjeh?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await context.Sanjehs.AsNoTracking().FirstOrDefaultAsync(u => u.GUID == guid, cancellationToken);
        }

        public async Task<PagedList<GetListByStandardIdAsyncDto>> GetListByStandardIdAsync(int PageNumber, int PageSize, Guid standardId, CancellationToken cancellationToken )
        {
            var query = context.Sanjehs
                .Where(x => x.StandardGUID == standardId)
                .Select(x => new GetListByStandardIdAsyncDto
                {
                    Title = x.Title,
                    Code = x.Code,
                    WeightedCoefficient = x.WeightedCoefficient,
                    IsDeleted = x.IsDeleted,
                    IsIdeal = x.IsIdeal,
                    HasAttachmentForArzyabi = x.HasAttachmentForArzyabi,
                    SortOrder = x.SortOrder,
                    SanjehLevelId = x.SanjehLevelId,
                    GUID = x.GUID,
                    EtebarDorehGuid = x.EtebarDorehGUID,
                    OrgTypeGuid = x.EtebarDoreh.OrgTypeGUID
                })
                .AsNoTracking()
                .OrderBy(x => x.SortOrder);

            // Paginate the IQueryable
            return await PagedList<GetListByStandardIdAsyncDto>.Paginate(
                source: query,
                pageNumber: PageNumber,
                pageSize: PageSize,
                cancellationToken: cancellationToken);
        }



        public async Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken)
        {
            var breadCrumbs = new List<GetBreadCrumbDto>();

            // Fetch Sanjeh Data
            var sanjehData = await context.Sanjehs
                .Where(s => s.GUID == guid)
                .Select(s => new
                {
                    Guid = s.GUID,
                    Title = s.Title,
                    StandardGUID = s.StandardGUID
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (sanjehData != null)
            {
                breadCrumbs.Add(new GetBreadCrumbDto
                {
                    Guid = sanjehData.Guid,
                    Title = sanjehData.Title,
                    GuidType = "sanjeh"
                });

                if (sanjehData.StandardGUID != null)
                {
                    var standardData = await context.Standards
                        .Where(s => s.GUID == sanjehData.StandardGUID)
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
                                                            GuidType = "orgtype"
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
                }
            }

            return breadCrumbs;
        }


        public async Task<List<GetSanjehListInArzyabiDakheliDto>> GetSanjehListInArzyabiDakheli(GetSanjehListInArzyabiDakheliQuery query, CancellationToken cancellationToken = default)
        {
            #region Old
            //return await context.AccreditationalInstanceAnswers
            //    .Include(san => san.Sanjeh)
            //    .Where(anw => anw.Sanjeh != null
            //    && anw.Sanjeh.StandardGUID == query.StandardGUID
            //    && anw.Sanjeh.SanjehLevelId == query.SanjehLevelId
            //    ).
            //    Select( Answer => new GetSanjehListInArzyabiDakheliDto
            //    {
            //        AccreditationInstanceAnswerId = Answer.AccreditationInstanceGUID,
            //        SanjehId = Answer.SanjehGUID,
            //        SanjehTitel = Answer.Sanjeh.Title,
            //        Result = Answer.Result,
            //        AnswerList= GetSanjehValidResault(Answer.AccreditationalInstance.CalculationVersionId),
            //        AnswerText = Answer.AnswerText,
            //        FullUserName = Answer.User!=null? Answer.User.FirstName + " " + Answer.User.LastName:""
            //    }
            //    ).ToListAsync();
            #endregion

            // Fetch data first
            var answers = await context.AccreditationalInstanceAnswers
                .Include(san => san.Sanjeh)
                .Where(anw => anw.Sanjeh != null
                       && anw.Sanjeh.StandardGUID == query.StandardGUID
                       && anw.Sanjeh.SanjehLevelId == query.SanjehLevelId
                       && anw.AccreditationInstanceGUID == query.AccInstanceID)
                .OrderBy(answer => answer.Sanjeh.SortOrder)
                .ToListAsync(cancellationToken);

            foreach (var answer in answers)
            {
                if (answer.User == null && answer.UserGUID != null)
                {
                    await context.Entry(answer).Reference(a => a.User).LoadAsync(cancellationToken);
                }
            }

            var accreditationStatusList = await context.AccreditationInstances
                .Where(x => x.EtebarDorehGUID == query.EtebardorehId && x.InstanceTypeId == 1)
                 .AsNoTracking()
                .Select(accIns => accIns.AccreditationInstanceStatusType.IsLocked)
                .ToListAsync();

            // Transform the data into DTOs in-memory
            var result = answers.Select(answer => new GetSanjehListInArzyabiDakheliDto
            {
                AccreditationInstanceAnswerId = answer.GUID,
                SanjehId = answer.SanjehGUID,
                SanjehTitel = answer.Sanjeh?.Title, // Safe access since it's in-memory
                Result = answer.Result ?? null, // Use null-coalescing operator for clarity
                IsLock = accreditationStatusList.Any(),
                //AnswerList = context.SanjehValidResaults
                //    .Where(validres => validres.CalculationVersionId == (answer.AccreditationalInstance != null ? answer.AccreditationalInstance.CalculationVersionId : (int?)null))
                //    .Select(s => new SanjehValidResaultDto
                //    {
                //        ResaultText = s.ResaultText,
                //        ResaultValue = s.ResaultValue
                //    }).ToList(), // The LINQ query is performed in memory
                AnswerText = answer.AnswerText ?? null, // Use null-coalescing operator for clarity
                FullUserName = answer.User != null ? $"{answer.User.FirstName} {answer.User.LastName}" : "" // Concatenating names safely
            }).ToList();

            return result;
        }

        public async Task<List<GetSanjehValidResaultDto>> GetSanjehValidResault(GetSanjehValidResaultQuery query, CancellationToken cancellationToken = default)
        {
            //       context.SanjehValidResaults
            //       .Include
            //       .Where(validres => validres.CalculationVersionId == (answer.AccreditationalInstance != null ? answer.AccreditationalInstance.CalculationVersionId : (int?)null))
            //       .Select(s => new SanjehValidResaultDto
            //       {
            //           ResaultText = s.ResaultText,
            //           ResaultValue = s.ResaultValue
            //}).ToList(), // The LINQ query is performed in memory


            var Result = await
               (
               from ValidResault in context.SanjehValidResults
               join AccInstance in context.AccreditationInstances
               on ValidResault.CalculationVersionId equals AccInstance.CalculationVersionId
               where AccInstance.GUID == query.AccInstanceID
               select new GetSanjehValidResaultDto
               {
                   ResaultText = ValidResault.ResultText,
                   ResaultValue = ValidResault.ResultValue,
               }
              ).ToListAsync();
            GetSanjehValidResaultDto NoResponse = new GetSanjehValidResaultDto();
            NoResponse.ResaultText = "بدون پاسخ";
            NoResponse.ResaultValue = -2;
            Result.Add(NoResponse);

            return Result;
        }

       
    }
}
