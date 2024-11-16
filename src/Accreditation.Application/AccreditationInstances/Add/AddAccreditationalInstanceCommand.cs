using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.Add;

public sealed record AddAccreditationalInstanceCommand(Guid EtebarDorehGUID,
                                                  Guid OrganizationGuid,
                                                  int InstanceTypeId,
                                                  DateOnly? FromDate,
                                                  DateOnly? ToDate,
                                                  Guid? ArzyabSarparastGuid,
                                                  Guid? AccreditationInstanceGuid) : ICommand<Guid>;
