using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class EvaluationArzyabFieldRepository(AccreditationDbContext _context) : IEvaluationArzyabFieldRepository
{
    public void Add(EvaluationArzyabField evaluationArzyabField)
    {
        _context.Add(evaluationArzyabField);
    }

    public void Delete(EvaluationArzyab evaluationArzyab)
    {
        _context.EvaluationArzyabFields.RemoveRange(evaluationArzyab.EvaluationArzyabFields);
    }

    public async Task<List<EvaluationArzyabField>> GetAll(Guid evaluationArzyabGUID)
    {
      return await _context.EvaluationArzyabFields.AsNoTracking().Where(x => x.EvaluationArzyabGUID == evaluationArzyabGUID).ToListAsync();
    }
}