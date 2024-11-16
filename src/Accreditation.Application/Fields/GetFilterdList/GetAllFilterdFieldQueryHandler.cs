using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using SharedKernel;

namespace Accreditation.Application.Fields.GetFilterdList;

internal sealed class GetAllFilterdFieldQueryHandler
    : IQueryHandler<GetAllFilterdFieldQuery, List<GetAllFilteredFieldQueryDto>>
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetAllFilterdFieldQueryHandler(IFieldRepository fieldRepository,
                                   IEvaluationArzyabRepository evaluationArzyabRepository,
                                   IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _fieldRepository = fieldRepository;
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<List<GetAllFilteredFieldQueryDto>>> Handle(
        GetAllFilterdFieldQuery request,
        CancellationToken cancellationToken)
    {
        var accInstance = await _accreditationInstanceRepository.FindAsync(request.AccreditationalInstaneGuid);
        var evaluationFields = await _evaluationArzyabRepository.GetAll(request.AccreditationalInstaneGuid, cancellationToken);
        return await _fieldRepository.GetAllFilteredByEtebarDorehGuidAsync(request.EtebardorehGuid, evaluationFields.SelectMany(x => x.FieldIds).ToList(),accInstance.InstanceTypeId);

    }
}

