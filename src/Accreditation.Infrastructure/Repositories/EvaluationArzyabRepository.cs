using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Domain.Common.Enums;
using Accreditation.Domain.EvaluationArzyabs.Dtos;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class EvaluationArzyabRepository(AccreditationDbContext _context) : IEvaluationArzyabRepository
{
    public void Add(EvaluationArzyab evaluationArzyab)
    {
        _context.Add(evaluationArzyab);
    }
    public void Delete(EvaluationArzyab evaluationArzyab)
    {
        _context.EvaluationArzyabFields.RemoveRange(evaluationArzyab.EvaluationArzyabFields);
        _context.EvaluationArzyabs.Remove(evaluationArzyab);
    }

    public async Task<EvaluationArzyab?> FindAsync(Guid evaluationArzyabGuid, CancellationToken cancellationToken = default)
    {
        return await _context.EvaluationArzyabs
             .Include(e => e.EvaluationArzyabFields)
            .FirstOrDefaultAsync(e => e.GUID == evaluationArzyabGuid);
    }
    public async Task<List<EvaluationArzyab>> GetAllBasedAccins(Guid accreditationalInstanceGuid, CancellationToken cancellationToken)
    {
        return await _context.EvaluationArzyabs
            .Include(x => x.User)
            .Include(x => x.EvaluationArzyabFields)
            .ThenInclude(x => x.Field)
            .AsNoTracking()
            .Where(x => x.AccreditationInstanceGUID == accreditationalInstanceGuid)
            .ToListAsync();
    }
    public async Task<List<GetAllEvaluationArzyabsDto>> GetAll(Guid accreditationalInstanceGuid, CancellationToken cancellationToken)
    {
        return await  _context.EvaluationArzyabs
            .Include(x => x.User)
            .Include(x => x.EvaluationArzyabFields)
            .ThenInclude(x => x.Field)
            .AsNoTracking()
            .Where(x => x.AccreditationInstanceGUID == accreditationalInstanceGuid)
            .Select(u => new GetAllEvaluationArzyabsDto
            {
                ArzyabName = u.User.FirstName + " " + u.User.LastName,
                ArzyabGuid = u.ArzyabUserGUID,
                FieldIds = u.EvaluationArzyabFields.Select(x => x.FieldGuid).ToList(),
                FieldDtos = u.EvaluationArzyabFields.Select( c => new FieldDto
                {
                    Guid = c.FieldGuid,
                    Name = c.Field.Title
                }).ToList(),
                Guid = u.GUID,
                MobileNo = u.User.Mobile,
                NationalCode = u.User.NationalCode,
                RoleName = ((ArzyabRole)u.ArzyabRoleId).GetDescription(),
                RoleId = u.ArzyabRoleId
            })
            .ToListAsync();
    }
}
