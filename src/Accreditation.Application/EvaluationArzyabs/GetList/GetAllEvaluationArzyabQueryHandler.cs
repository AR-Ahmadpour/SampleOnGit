using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Domain.EvaluationArzyabs.Dtos;
using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs.GetList;

internal sealed class GetAllEvaluationArzyabQueryHandler
    : IQueryHandler<GetAllEvaluationArzyabQuery, List<GetAllEvaluationArzyabsDto>>
{
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;

    public GetAllEvaluationArzyabQueryHandler(IEvaluationArzyabRepository evaluationArzyabRepository)
    {
        _evaluationArzyabRepository = evaluationArzyabRepository;
    }
    public async Task<Result<List<GetAllEvaluationArzyabsDto>>> Handle(
        GetAllEvaluationArzyabQuery request,
        CancellationToken cancellationToken)
    {
        return await _evaluationArzyabRepository.GetAll(request.AccreditationalInstaneGuid, cancellationToken);

    }
}

