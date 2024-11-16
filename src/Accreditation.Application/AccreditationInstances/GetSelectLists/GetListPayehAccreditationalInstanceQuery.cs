using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.GetSelectLists;

public sealed record GetListPayehAccreditationalInstanceQuery(
    int InstanceTypeId,
    Guid EtebarDorehGUID,
    Guid OrganizationGuid) :
    IQuery<List<GetListPayehAccreditationalInstanceDto>>;
