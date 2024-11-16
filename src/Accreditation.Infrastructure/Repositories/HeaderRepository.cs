using Accreditation.Application.Common.Interfaces.Persistence.Headers;
using Accreditation.Application.Headers.GetList;
using Accreditation.Domain.OrgGerayeshes.Entities;
using Accreditation.Domain.Users;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Accreditation.Infrastructure.Repositories
{
    public sealed class HeaderRepository : IHeaderRepository
    {
        private readonly AccreditationDbContext _context;

        public HeaderRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<GetHeaderDto> GetHeader(Guid fieldGuid, Guid accreditationInstanceGuid, CancellationToken cancellationToken)
        {
            var fieldTitle = await _context.Fields
            .Where(f => f.GUID == fieldGuid)
            .Select(f => f.Title)
            .FirstOrDefaultAsync();



            var accreditationInstanceData = await (from accreditationInstance in _context.AccreditationInstances
              join organization in _context.Organizations
                  on accreditationInstance.OrganizationGUID equals organization.GUID
              join shahr in _context.Shahrs
                  on organization.ShahrId equals shahr.Id
              join university in _context.Universities
                  on organization.UniversityId equals university.Id
              join orgType in _context.OrgTypes
                  on organization.OrgTypeGUID equals orgType.GUID
              where accreditationInstance.GUID == accreditationInstanceGuid
              select new
              {
                  EtebarDorehTitle = accreditationInstance.EtebarDoreh.Title,
                  OrganizationName = organization.Name,
                  ShahrTitle = shahr.Title,
                  UniversityName = university.Title,
                  OrgTypeTitle = orgType.Title,
                  OrganizationGuid = organization.GUID,
                   
              }).FirstOrDefaultAsync();




            var evaluationArzyabGuids = await _context.EvaluationArzyabs
           .Where(ea => ea.AccreditationInstanceGUID == accreditationInstanceGuid)
           .Select(ea => ea.GUID)
           .ToListAsync(cancellationToken);

            // Initialize evaluationArzyabFieldGuid
            Guid? evaluationArzyabFieldGuid = null;



            foreach (var fieldEntity in await _context.EvaluationArzyabFields
            .Where(eaf => eaf.FieldGuid == fieldGuid)
            .ToListAsync(cancellationToken))
            {
                // Check if the EvaluationArzyabGUID is in the list of evaluationArzyabGuids
                bool isInList = evaluationArzyabGuids.Any(guid => guid == fieldEntity.EvaluationArzyabGUID);

                if (isInList)
                {
                    evaluationArzyabFieldGuid = fieldEntity.EvaluationArzyabGUID;
                    break;
                }
            }



            User? user = null;

            if (evaluationArzyabFieldGuid.HasValue && evaluationArzyabFieldGuid.Value != Guid.Empty)
            {
                user = await _context.Users
                    .Join(_context.EvaluationArzyabs, u => u.GUID, ea => ea.ArzyabUserGUID, (u, ea) => new { u, ea })
                    .Where(joined => joined.ea.GUID == evaluationArzyabFieldGuid)
                    .Select(joined => joined.u)
                    .FirstOrDefaultAsync(cancellationToken);
            }


            var headerDto = new GetHeaderDto
            {
                FieldTitle = fieldTitle,
                EtebarDorehTitle = accreditationInstanceData?.EtebarDorehTitle,
                OrganizationName = accreditationInstanceData?.OrganizationName,
                ShahrTitle = accreditationInstanceData?.ShahrTitle,
                UniversityName = accreditationInstanceData?.UniversityName,
                OrgTypeTitle = accreditationInstanceData?.OrgTypeTitle,
                ArzyabCredentials = $"{user?.FirstName}" +  $" {user?.LastName}" + $" ({user?.NationalCode})",
                UserGuid = user?.GUID,
                OrganizationGuid = accreditationInstanceData?.OrganizationGuid,
                AccreditationInstanceGuid = accreditationInstanceGuid,
                FieldGuid = fieldGuid
            };

            return headerDto;
        }
    }
}
