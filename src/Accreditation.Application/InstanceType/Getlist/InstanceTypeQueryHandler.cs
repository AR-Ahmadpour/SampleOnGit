using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.InstanceType;
using Accreditation.Domain.Common.Enums;
using SharedKernel;

namespace Accreditation.Application.InstanceType.Getlist;

public class InstanceTypeQueryHandler : IQueryHandler<InstanceTypeQuery, List<InstanseTypeResultDto>>
{
    private readonly IInstanceTypeRepository _instanceTypeRepository;
    private readonly IUserContext _userContext;

    public InstanceTypeQueryHandler(IInstanceTypeRepository instanceTypeRepository,
                                    IUserContext userContext)
    {
        _instanceTypeRepository = instanceTypeRepository;
        _userContext = userContext;
    }
    public async Task<Result<List<InstanseTypeResultDto>>> Handle(InstanceTypeQuery request,
                                                            CancellationToken cancellationToken)
    {
        var instanceTypes = await _instanceTypeRepository.GetSelectListInstanceTypeAsync(cancellationToken);

        if (_userContext.Role == RolesType.StaffChief.ToString() ||
            _userContext.Role == RolesType.StaffManager.ToString())
        {
            instanceTypes = instanceTypes.Where(x => x.IsActiveInStaff == true).ToList();
        }
        if (_userContext.Role == RolesType.UniMember.ToString())
        {
            instanceTypes = instanceTypes.Where(x => x.IsActiveInUniversity == true).ToList();
        }
        return instanceTypes.Select(x => new InstanseTypeResultDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();
    }
}
