using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using SharedKernel;

namespace Accreditation.Application.Fields.GetList;

internal sealed class GetAllFieldQueryHandler
    : IQueryHandler<GetAllFieldQuery, List<GetAllFieldQueryDto>>
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetAllFieldQueryHandler(IFieldRepository fieldRepository,
                                   IEvaluationArzyabRepository evaluationArzyabRepository,
                                   IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _fieldRepository = fieldRepository;
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<List<GetAllFieldQueryDto>>> Handle(
        GetAllFieldQuery request,
        CancellationToken cancellationToken)
    {
        var evaluationFields = await _evaluationArzyabRepository.GetAll(request.AccreditationalInstaneGuid, cancellationToken);
        var accInstance = await _accreditationInstanceRepository.FindAsync(request.AccreditationalInstaneGuid);
        return await _fieldRepository.GetAllByEtebarDorehGuidAsync(request.EtebardorehGuid, accInstance.InstanceTypeId);

    }
}

