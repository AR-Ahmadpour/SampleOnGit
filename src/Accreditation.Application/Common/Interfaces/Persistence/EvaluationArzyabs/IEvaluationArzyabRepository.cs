using Accreditation.Domain.EvaluationArzyabs.Dtos;
using Accreditation.Domain.EvaluationArzyabs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
public interface IEvaluationArzyabRepository
{
    Task<EvaluationArzyab> FindAsync(Guid evaluationArzyabGuid, CancellationToken cancellationToken);
    Task<List<GetAllEvaluationArzyabsDto>> GetAll(Guid accreditationalInstanceGuid, CancellationToken cancellationToken);
    void Add(EvaluationArzyab evaluationArzyab);
    void Delete(EvaluationArzyab evaluationArzyab);
    Task<List<EvaluationArzyab>> GetAllBasedAccins(Guid accreditationalInstanceGuid, CancellationToken cancellationToken);
}
