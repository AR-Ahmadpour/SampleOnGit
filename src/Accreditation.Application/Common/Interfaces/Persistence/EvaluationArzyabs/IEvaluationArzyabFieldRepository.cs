using Accreditation.Domain.EvaluationArzyabs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
public interface IEvaluationArzyabFieldRepository
{
    void Add(EvaluationArzyabField evaluationArzyabField);
    Task<List<EvaluationArzyabField>> GetAll(Guid EvaluationArzyabGUID);
    void Delete(EvaluationArzyab evaluationArzyabGuid);
}
