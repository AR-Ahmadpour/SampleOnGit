using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.Edit;
public sealed record EditAccInstanceCommand(Guid GUID,
                                           DateOnly FromDate,
                                           DateOnly ToDate,
                                           Guid? ArzyabSarparastGuid) : ICommand<Guid>;
