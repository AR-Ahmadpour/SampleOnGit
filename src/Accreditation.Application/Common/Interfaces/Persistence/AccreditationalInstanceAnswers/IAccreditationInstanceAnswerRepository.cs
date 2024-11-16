using Accreditation.Application.AccreditationalInstanceAnswers.Edit;
using Accreditation.Domain.AccreditationInstanceAnswers.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;

public interface IAccreditationInstanceAnswerRepository
{
    void Add(AccreditationalInstanceAnswer accreditationalInstanceAnswer);
    //Task<Guid> EdirResult(EditAccreditationalInstanceAnswerCommand command, CancellationToken cancellationToken);
    Task<List<AccreditationalInstanceAnswer>> GetListAccInstanceAnswersAsync(Guid accreditationalInstanceGuid);
    void Delete(AccreditationalInstanceAnswer accreditationalInstanceAnswer);
    Task<AccreditationalInstanceAnswer> FindAsyc(Guid GUID, CancellationToken cancellationToken);
}
