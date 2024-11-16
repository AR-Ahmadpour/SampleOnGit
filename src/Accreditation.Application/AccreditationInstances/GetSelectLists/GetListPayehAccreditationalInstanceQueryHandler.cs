using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.GetSelectLists;

internal sealed class GetListPayehAccreditationalInstanceQueryHandler
: IQueryHandler<GetListPayehAccreditationalInstanceQuery, List<GetListPayehAccreditationalInstanceDto>>
{

    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetListPayehAccreditationalInstanceQueryHandler(IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<List<GetListPayehAccreditationalInstanceDto>>> Handle(
        GetListPayehAccreditationalInstanceQuery request,
        CancellationToken cancellationToken)
    {
        return await _accreditationInstanceRepository.GetAllPayehAsync(
            request.InstanceTypeId,
            request.EtebarDorehGUID,
            request.OrganizationGuid,
            cancellationToken = default);
    }
}
