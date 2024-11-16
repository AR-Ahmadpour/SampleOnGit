using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.GetList;

public sealed record GetListAccreditationalInstanceQuery(
    int InstanceTypeId,
    Guid EtebarDorehGUID,
    Guid OrganizationGuid) :
    IQuery<List<GetListAccreditationalInstanceDto>>;
