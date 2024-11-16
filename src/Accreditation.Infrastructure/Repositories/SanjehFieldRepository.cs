using Accreditation.Application.Common.Interfaces.Persistence.SanjehFields;
using Accreditation.Application.NotNaSanjehs.GetList;
using Accreditation.Application.SanjehFields.GetById;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.SanjehFields.Entities;
using Accreditation.Infrastructure.Database;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class SanjehFieldRepository(AccreditationDbContext context) : ISanjehFieldRepository
    {
        public void Add(SanjehField sanjehField)
        {
            context.SanjehFields.Add(sanjehField);
        }

        public async Task<SanjehField?> FindAsync(Guid snj_GUID, Guid FieldGUID, CancellationToken cancellationToken)
        {
            return await context.SanjehFields
                .Where(sf => sf.SanjehGUID == snj_GUID && sf.FieldGUID == FieldGUID)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<GetSanjehsByFieldDto>> GetAllSanjehsByFieldId(Guid fieldGuid,Guid? mehvarId, CancellationToken cancellationToken = default)
        {

            // Start with the base query for SanjehGUIDs based on FieldGUID
            var query = context.SanjehFields
                .Where(sf => sf.FieldGUID == fieldGuid)
                .Select(sf => sf.SanjehGUID);

            // If mehvarId is provided, filter the results based on MehvarGUID
            if (mehvarId.HasValue)
            {
                query = from s in context.Sanjehs
                        join sf in query on s.GUID equals sf
                        where s.MehvarGUID == mehvarId
                        select s.GUID;
            }

            // Execute the query and map to GetSanjehsByFieldDto
            var result = await query.Select(guid => new GetSanjehsByFieldDto { Guid = guid }).ToListAsync(cancellationToken);

            return result;
        }

        public async Task<List<SanjehField>> GetByFieldGuidAsync(Guid fieldGuid, CancellationToken cancellationToken)
        {
            return await context.Set<SanjehField>()
                    .Where(n => n.FieldGUID == fieldGuid)
                    .ToListAsync(cancellationToken);
        }

        public void RemoveRange(IEnumerable<SanjehField> sanjehField)
        {
            context.Set<SanjehField>().RemoveRange(sanjehField);
        }
    }
}
