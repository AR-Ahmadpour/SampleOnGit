using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Organizations.GetById;

public sealed record GetOrganizationByIdQuery(Guid GUID) : IQuery<OrganizationDto>;

