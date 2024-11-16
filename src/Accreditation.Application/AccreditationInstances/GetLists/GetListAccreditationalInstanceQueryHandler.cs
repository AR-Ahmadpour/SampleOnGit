using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.GetList;

internal sealed class GetListAccreditationalInstanceQueryHandler
: IQueryHandler<GetListAccreditationalInstanceQuery, List<GetListAccreditationalInstanceDto>>
{

    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetListAccreditationalInstanceQueryHandler(IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<List<GetListAccreditationalInstanceDto>>> Handle(
        GetListAccreditationalInstanceQuery request,
        CancellationToken cancellationToken)
    {
        return await _accreditationInstanceRepository.GetAllAsync(
            request.InstanceTypeId,
            request.EtebarDorehGUID,
            request.OrganizationGuid,
            cancellationToken = default);
    }
}
