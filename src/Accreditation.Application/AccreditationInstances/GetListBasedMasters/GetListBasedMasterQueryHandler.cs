using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.GetListBasedMasters;
internal sealed class GetListBasedMasterQueryHandler
: IQueryHandler<GetListBasedMasterQuery, List<GetListBasedMasterDto>>
{

    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetListBasedMasterQueryHandler(IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<List<GetListBasedMasterDto>>> Handle(
        GetListBasedMasterQuery request,
        CancellationToken cancellationToken)
    {
        return await _accreditationInstanceRepository.FindAllBasedPayehAsync(request.AccInstanceGuid, cancellationToken = default);
    }
}