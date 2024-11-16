using Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID;
using Accreditation.Application.AccreditationInstances.GetList;
using Accreditation.Application.AccreditationInstances.GetListBasedMasters;
using Accreditation.Application.AccreditationInstances.GetSelectLists;
using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Common.Enums;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.Organizations.Entities;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Domain.Universites.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Accreditation.Infrastructure.Repositories;
internal sealed class AccreditationInstanceRepository(AccreditationDbContext context) : IAccreditationInstanceRepository
{
    public void Add(AccreditationInstance evaluationCalendar)
    {
        context.Add(evaluationCalendar);
    }

    public async Task<AccreditationInstance?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.AccreditationInstances.FindAsync(id, cancellationToken);
    }

    public async Task<AccreditationInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.AccreditationInstances
                            //.Include(x => x.EvaluationArzyabs)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.GUID == id, cancellationToken);
    }

    public async Task<List<GetListAccreditationalInstanceDto>> GetAllAsync(int instanceTypeId,
                                                                                Guid etebarDorehGUID,
                                                                                Guid organizationId,
                                                                                CancellationToken cancellationToken = default)
    {
        return await context.AccreditationInstances
        .Include(x => x.EtebarDoreh)
        .Include(x => x.InstanceType)
        .Include(x => x.Organization)
        .ThenInclude(x => x.OrgGerayesh)
        .ThenInclude(x => x.OrgType)
         .Include(x => x.EvaluationArzyabs)
          .ThenInclude(x => x.User)
        .Where(x => x.InstanceTypeId == instanceTypeId &&
                    x.OrganizationGUID == organizationId &&
                    x.EtebarDorehGUID == etebarDorehGUID)
        .OrderByDescending(x => x.CreateDateTime)
        .Select(x => new GetListAccreditationalInstanceDto
        {
            Guid = x.GUID,
            AccreditationInstancePayehGuid = x.MasterGUID,
            OrganizationGuid = x.OrganizationGUID,
            OrganizationName = x.Organization.Name,
            InstanceTypeName = x.InstanceType.Title,
            DateFrom = x.FromDate,
            DateTo = x.ToDate,
            SarparastGuid = x.EvaluationArzyabs
                .Where(e => e.ArzyabRoleId == 1)
                .Select(e => (Guid?)e.ArzyabUserGUID)
                .FirstOrDefault(),
            SarparastName = x.EvaluationArzyabs
                    .Where(e => e.ArzyabRoleId == 1)
                    .Select(e => e.User.FirstName + " " + e.User.LastName)
                    .FirstOrDefault(),
            PercentGrade = null,
            Status = null,
            OrgTypeName = x.Organization.OrgGerayesh.OrgType.Title
        })
        .AsNoTracking()
        .ToListAsync();
    }

    public void Delete(AccreditationInstance accreditationInstance)
    {
        context.AccreditationInstances.Remove(accreditationInstance);
    }

    public async Task<List<GetListPayehAccreditationalInstanceDto>> GetAllPayehAsync(int instanceTypeId,
                                                                               Guid etebarDorehGUID,
                                                                               Guid organizationGuid,
                                                                               CancellationToken cancellationToken = default)
    {
        var query = context.AccreditationInstances
          .Include(x => x.CreatedUser)
          .Include(x => x.EtebarDoreh)
          .Include(x => x.InstanceType)
          .Include(x => x.Organization)
              .ThenInclude(x => x.OrgGerayesh)
                  .ThenInclude(x => x.OrgType)
          .Include(x => x.EvaluationArzyabs)
          .ThenInclude(x => x.User)
          .OrderByDescending(x => x.CreateDateTime)
          .Where(x => x.OrganizationGUID == organizationGuid &&
                      x.EtebarDorehGUID == etebarDorehGUID);

        var instanseTypeName = "";

        if ((int)InstanceTypes.ArzyabiJame == instanceTypeId)
        {
            query = query.Where(x => x.InstanceTypeId == (int)InstanceTypes.ArzyabiDakheli);
            instanseTypeName = InstanceTypes.ArzyabiDakheli.GetDescription();
        }
        else if ((int)InstanceTypes.ArzyabiMojadad == instanceTypeId)
        {
            query = query.Where(x => x.InstanceTypeId == (int)InstanceTypes.ArzyabiJame);
            instanseTypeName = InstanceTypes.ArzyabiJame.GetDescription();
        }
        else if ((int)InstanceTypes.RastiAzmai == instanceTypeId)
        {
            query = query.Where(x => x.InstanceTypeId == (int)InstanceTypes.ArzyabiMojadad);
            instanseTypeName = InstanceTypes.ArzyabiMojadad.GetDescription();

            query = query.Where(x => x.InstanceTypeId == (int)InstanceTypes.ArzyabiJame);
            instanseTypeName = InstanceTypes.ArzyabiJame.GetDescription();
        }
        else if ((int)InstanceTypes.ArzyabiIdeal == instanceTypeId)
        {
            query = query.Where(x => x.InstanceTypeId == (int)InstanceTypes.ArzybiDakheliIdeal);
            instanseTypeName = InstanceTypes.ArzybiDakheliIdeal.GetDescription();
        }
        else
        {
            return new List<GetListPayehAccreditationalInstanceDto>();
        }

        return await query
            .Select(x => new GetListPayehAccreditationalInstanceDto
            {
                Guid = x.GUID,
                OrganizationGuid = x.OrganizationGUID,
                OrganizationName = x.Organization.Name,
                InstanceTypeName = x.InstanceType.Title,
                DateFrom = x.FromDate,
                DateTo = x.ToDate,
                SarparastGuid = x.EvaluationArzyabs
                .Where(e => e.ArzyabRoleId == 1)
                .Select(e => (Guid?)e.ArzyabUserGUID)
                .FirstOrDefault(),

                EtebarDoreh = x.EtebarDoreh.Title,
                CreatedUserName = x.CreatedUser.FirstName + " " + x.CreatedUser.LastName,
                SarparastName = x.EvaluationArzyabs
                    .Where(e => e.ArzyabRoleId == 1)
                    .Select(e => e.User.FirstName + " " + e.User.LastName)
                    .FirstOrDefault(),
                PercentGrade = null,
                Status = null,
                OrgTypeName = x.Organization.OrgGerayesh.OrgType.Title
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<bool> FindIsPayehAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.AccreditationInstances.AnyAsync(x => x.MasterGUID == id);
    }

    public Task<List<GetListBasedMasterDto>> FindAllBasedPayehAsync(Guid masterGuid, CancellationToken cancellationToken = default)
    {
        return context.AccreditationInstances.Where(x => x.MasterGUID == masterGuid)
            .AsNoTracking()
            .Select(c => new GetListBasedMasterDto
            {
                DateFrom = c.FromDate,
                DateTo = c.ToDate,
                Guid = c.GUID,
                InstanceType = c.InstanceTypeId
            }).ToListAsync();
    }


    public async Task<List<GetAccreditationInstanceByEtebarDorehIdQueryDto>> GetAccreditationInstanceByEtebarDorehId
                                                            (GetAccreditationInstanceByEtebarDorehIdQuery query
                                                            , CancellationToken cancellationToken = default)
    {
        return await context.AccreditationInstances
            .Include(_ => _.Organization.University)
            .Where(x => x.EtebarDorehGUID == query.EtebarDorehId && x.InstanceTypeId == 1)
        .AsNoTracking()
        .Select(AccIns => new GetAccreditationInstanceByEtebarDorehIdQueryDto
        {
            GUID = AccIns.GUID,
            ArzyabiType = AccIns.InstanceType.Title,
            StartDate = AccIns.FromDate.ToString(),
            OrganizationName = AccIns.Organization.Name,
            state = AccIns.AccreditationInstanceStatusType.Title,
            ActionLink = AccIns.AccreditationInstanceStatusType.IsLocked == true ? "مشاهده نتایج" : "مشاهده و ویرایش نتایج ",
            SendStatus = AccIns.AccreditationInstanceStatusType.IsLocked,
            UniversityName = AccIns.Organization.University.Title


            // 

        }).ToListAsync();
    }

    public async Task<bool> Any(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.AccreditationInstances.AnyAsync(x => x.GUID == id, cancellationToken);
    }

    public async Task<GetBodyByAccreditationInstanceGuidDto> GetBody(Guid accreditationInstanceGuid, Guid FieldGuid, CancellationToken cancellationToken)
    {


        var sanjehTitlesAndGuids = await context.SanjehFields
           .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
           .Join(context.Sanjehs,
                 sf => sf.SanjehGUID,
                 s => s.GUID,
                 (sf, s) => new { s.GUID, s.Title })
           .ToListAsync(cancellationToken);

        // Extract GUIDs and create a list for sorting later
        var sanjehGuids = sanjehTitlesAndGuids.Select(stg => stg.GUID).ToList();

        var NaList = new List<bool>();

        foreach (var sanjeh in sanjehGuids)
        {
            var exist = await context.NotNaSanjehs
                .AnyAsync(x => x.SanjehGUID == sanjeh,cancellationToken);

            NaList.Add(exist);
        }

        // Retrieve all child results for the given AccreditationInstanceGUID
        var allResults = await context.AccreditationalInstanceAnswers
            .Where(answer => answer.AccreditationInstanceGUID == accreditationInstanceGuid)
            .ToListAsync(cancellationToken);

        // Manually filter and sort child results based on the SanjehGUIDs
        var resultChild = new List<decimal?>();
        var PkOfResultChild = new List<Guid?>();
        foreach (var sanjehGuid in sanjehGuids) 
        {
            var result = allResults.FirstOrDefault(r => r.SanjehGUID == sanjehGuid);
            resultChild.Add(result?.Result);
            PkOfResultChild.Add(result?.GUID);
        }

        var masterGuid = await context.AccreditationInstances
           .Where(i => i.GUID == accreditationInstanceGuid)
           .Select(i => i.MasterGUID)
           .FirstOrDefaultAsync(cancellationToken);

        // Retrieve the master result if applicable
        var resultFather = new List<decimal?>();
        if (masterGuid == null)
        {
            // If masterGuid is null, fill resultFather with null values based on the count of sanjehGuids
            resultFather.AddRange(Enumerable.Repeat<decimal?>(null, sanjehGuids.Count));
        }
        else
        {
            // Retrieve all father results for the master GUID
            var allFatherResults = await context.AccreditationalInstanceAnswers
                .Where(a => a.AccreditationInstanceGUID == masterGuid)
                .ToListAsync(cancellationToken);

            // Manually filter and sort father results based on the SanjehGUIDs
            foreach (var sanjehGuid in sanjehGuids)
            {
                var result = allFatherResults.FirstOrDefault(r => r.SanjehGUID == sanjehGuid);
                resultFather.Add(result?.Result);
            }
        }


        var calculationVersionId = await context.AccreditationInstances
       .Where(ai => ai.GUID == accreditationInstanceGuid)
       .Select(ai => ai.CalculationVersionId)
       .FirstOrDefaultAsync(cancellationToken);

        // Step 5: Fetch ResultText and ResultValue from SanjehValidResult for the corresponding CalculationVersionId
        var validResults = await context.SanjehValidResults
            .Where(vr => vr.CalculationVersionId == calculationVersionId)
            .Select(vr => new { vr.ResultText, vr.ResultValue })
            .ToListAsync(cancellationToken);

        var validResultsDictionary = validResults.ToDictionary(vr => vr.ResultValue, vr => vr.ResultText);


        var resultChildMapped = resultChild.Select(rc =>
                  rc.HasValue && validResultsDictionary.ContainsKey(rc.Value)
                  ? validResultsDictionary[rc.Value]
                  : null
                  ).ToList();

        var resultFatherMapped = resultFather.Select(rf =>
                rf.HasValue && validResultsDictionary.ContainsKey(rf.Value)
                ? validResultsDictionary[rf.Value]
                : null
                ).ToList();


        var resultForColumnO = await context.SanjehValidResults
            .Where(x => x.CalculationVersionId == calculationVersionId)
           .Select(x => x.ResultText).ToListAsync();

        var resultForDB = await context.SanjehValidResults
            .Where(x => x.CalculationVersionId == calculationVersionId)
           .Select(x => x.ResultValue).ToListAsync();


        var masterofInstanceguid = await context.AccreditationInstances
            .Where(x => x.GUID == accreditationInstanceGuid)
            .Select(x => x.MasterGUID)
            .FirstOrDefaultAsync(cancellationToken);

        // Fetch the universityFinalAnswers with relevant data
        var universityFinalAnswers = await context.AccreditationalInstanceAnswers
            .Where(aia => aia.AccreditationInstanceGUID == masterofInstanceguid)
            .Join(context.SanjehFields,
                  aia => aia.SanjehGUID,
                  sf => sf.SanjehGUID,
                  (aia, sf) => new { aia.SanjehGUID, aia.Universityopinion, sf.FieldGUID, sf.IsDeleted })
            .Where(joined => joined.FieldGUID == FieldGuid && !joined.IsDeleted)
            .ToListAsync(cancellationToken);

        // Create a dictionary to map SanjehGUID to Universityopinion
        var universityFinalAnswersDict = universityFinalAnswers
            .ToDictionary(x => x.SanjehGUID, x => x.Universityopinion);

        // Align universityFinalAnswers with sanjehGuids
        var universityFinalAnswersSorted = sanjehGuids
            .Select(guid => universityFinalAnswersDict.TryGetValue(guid, out var opinion) ? opinion : null)
            .ToList();

        var Result = new GetBodyByAccreditationInstanceGuidDto
        {
            Sanjehs = sanjehTitlesAndGuids.Select(stg => stg.Title).ToList(),

            Standards = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.Standard.Title)
                .ToListAsync(cancellationToken),

            ZirMehvars = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.ZirMehvar.Title)
                .ToListAsync(cancellationToken),

            Mehvars = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.Mehvar.Title)
                .ToListAsync(cancellationToken),

            ResultFather = resultFatherMapped,
            ResultChild = resultChildMapped,
            PkOfResultChild = PkOfResultChild,
            ResultForColumnO = resultForColumnO,
            ResultForDb = resultForDB,
            NaLists = NaList,
            FinalUniversityAnswer = universityFinalAnswersSorted,
            FinalPeriod = Enumerable.Repeat<string>(null, sanjehGuids.Count).ToList(),
        };

        return Result;


    }

    public async Task<GetBodyByAccreditationInstanceGuidMommayeziDto> GetBodyMommayezi(Guid accreditationInstanceGuid, Guid FieldGuid, CancellationToken cancellationToken)
    {
        var sanjehTitlesAndGuids = await context.SanjehFields
           .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
           .Join(context.Sanjehs,
                 sf => sf.SanjehGUID,
                 s => s.GUID,
                 (sf, s) => new { s.GUID, s.Title })
           .ToListAsync(cancellationToken);

        // Extract GUIDs and create a list for sorting later
        var sanjehGuids = sanjehTitlesAndGuids.Select(stg => stg.GUID).ToList();

        var NaList = new List<bool>();

        foreach (var sanjeh in sanjehGuids)
        {
            var exist = await context.NotNaSanjehs
                .AnyAsync(x => x.SanjehGUID == sanjeh, cancellationToken);

            NaList.Add(exist);
        }

        // Retrieve all child results for the given AccreditationInstanceGUID
        var allResults = await context.AccreditationalInstanceAnswers
            .Where(answer => answer.AccreditationInstanceGUID == accreditationInstanceGuid)
            .ToListAsync(cancellationToken);

        // Manually filter and sort child results based on the SanjehGUIDs
        var resultChild = new List<decimal?>();
        //var PkOfResultChild = new List<Guid?>();
        foreach (var sanjehGuid in sanjehGuids)
        {
            var result = allResults.FirstOrDefault(r => r.SanjehGUID == sanjehGuid);
            resultChild.Add(result?.Result);
            //PkOfResultChild.Add(result?.GUID);
        }

        //taking care of the Audit result and the PK
        var AuditResult = new List<decimal?>();
        var PkOfResultChild = new List<Guid?>();
        foreach (var sanjehGuid in sanjehGuids)
        {
            var result = allResults.FirstOrDefault(r => r.SanjehGUID == sanjehGuid);
            AuditResult.Add(result?.AuditResult);
            PkOfResultChild.Add(result?.GUID);
        }

        var masterGuid = await context.AccreditationInstances
           .Where(i => i.GUID == accreditationInstanceGuid)
           .Select(i => i.MasterGUID)
           .FirstOrDefaultAsync(cancellationToken);

        // Retrieve the master result if applicable
        var resultFather = new List<decimal?>();
        if (masterGuid == null)
        {
            // If masterGuid is null, fill resultFather with null values based on the count of sanjehGuids
            resultFather.AddRange(Enumerable.Repeat<decimal?>(null, sanjehGuids.Count));
        }
        else
        {
            // Retrieve all father results for the master GUID
            var allFatherResults = await context.AccreditationalInstanceAnswers
                .Where(a => a.AccreditationInstanceGUID == masterGuid)
                .ToListAsync(cancellationToken);

            // Manually filter and sort father results based on the SanjehGUIDs
            foreach (var sanjehGuid in sanjehGuids)
            {
                var result = allFatherResults.FirstOrDefault(r => r.SanjehGUID == sanjehGuid);
                resultFather.Add(result?.Result);
            }
        }


        var calculationVersionId = await context.AccreditationInstances
       .Where(ai => ai.GUID == accreditationInstanceGuid)
       .Select(ai => ai.CalculationVersionId)
       .FirstOrDefaultAsync(cancellationToken);

        // Step 5: Fetch ResultText and ResultValue from SanjehValidResult for the corresponding CalculationVersionId
        var validResults = await context.SanjehValidResults
            .Where(vr => vr.CalculationVersionId == calculationVersionId)
            .Select(vr => new { vr.ResultText, vr.ResultValue })
            .ToListAsync(cancellationToken);

        var validResultsDictionary = validResults.ToDictionary(vr => vr.ResultValue, vr => vr.ResultText);


        var resultChildMapped = resultChild.Select(rc =>
                  rc.HasValue && validResultsDictionary.ContainsKey(rc.Value)
                  ? validResultsDictionary[rc.Value]
                  : null
                  ).ToList();

        var resultFatherMapped = resultFather.Select(rf =>
                rf.HasValue && validResultsDictionary.ContainsKey(rf.Value)
                ? validResultsDictionary[rf.Value]
                : null
                ).ToList();


        var resultForColumnO = await context.SanjehValidResults
            .Where(x => x.CalculationVersionId == calculationVersionId)
           .Select(x => x.ResultText).ToListAsync();

        var resultForDB = await context.SanjehValidResults
            .Where(x => x.CalculationVersionId == calculationVersionId)
           .Select(x => x.ResultValue).ToListAsync();


        var masterofInstanceguid = await context.AccreditationInstances
            .Where(x => x.GUID == accreditationInstanceGuid)
            .Select(x => x.MasterGUID)
            .FirstOrDefaultAsync(cancellationToken);

        // Fetch the universityFinalAnswers with relevant data
        var universityFinalAnswers = await context.AccreditationalInstanceAnswers
            .Where(aia => aia.AccreditationInstanceGUID == masterofInstanceguid)
            .Join(context.SanjehFields,
                  aia => aia.SanjehGUID,
                  sf => sf.SanjehGUID,
                  (aia, sf) => new { aia.SanjehGUID, aia.Universityopinion, sf.FieldGUID, sf.IsDeleted })
            .Where(joined => joined.FieldGUID == FieldGuid && !joined.IsDeleted)
            .ToListAsync(cancellationToken);

        // Create a dictionary to map SanjehGUID to Universityopinion
        var universityFinalAnswersDict = universityFinalAnswers
            .ToDictionary(x => x.SanjehGUID, x => x.Universityopinion);

        // Align universityFinalAnswers with sanjehGuids
        var universityFinalAnswersSorted = sanjehGuids
            .Select(guid => universityFinalAnswersDict.TryGetValue(guid, out var opinion) ? opinion : null)
            .ToList();

        var Result = new GetBodyByAccreditationInstanceGuidMommayeziDto
        {
            Sanjehs = sanjehTitlesAndGuids.Select(stg => stg.Title).ToList(),

            Standards = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.Standard.Title)
                .ToListAsync(cancellationToken),

            ZirMehvars = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.ZirMehvar.Title)
                .ToListAsync(cancellationToken),

            Mehvars = await context.SanjehFields
                .Where(sf => sf.FieldGUID == FieldGuid && sf.IsDeleted == false)
                .Join(context.Sanjehs,
                      sf => sf.SanjehGUID,
                      s => s.GUID,
                      (sf, s) => s.Mehvar.Title)
                .ToListAsync(cancellationToken),

            ResultFather = resultFatherMapped,
            ResultChild = resultChildMapped,
            AuditResult = AuditResult,
            PkOfResultChild = PkOfResultChild,
            ResultForColumnO = resultForColumnO,
            ResultForDb = resultForDB,
            NaLists = NaList,
            FinalUniversityAnswer = universityFinalAnswersSorted,
            FinalPeriod = Enumerable.Repeat<string>(null, sanjehGuids.Count).ToList(),
        };

        return Result;
    }
}