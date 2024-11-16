using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using SharedKernel;

namespace Accreditation.Application.Organizations.GetById;

internal sealed class GetOrganizationByIdQueryHandler
    : IQueryHandler<GetOrganizationByIdQuery, OrganizationDto>
{

    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    public async Task<Result<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.GUID, cancellationToken);

        if (organization == null)
        {
            return Result.Failure<OrganizationDto>(OrganizationErrors.NotFound);
        }

        return organization;
    }
}
