using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EtebarDorehs.GetById;

public sealed record GetEtebarDorehByIdQuery(Guid GUID) : IQuery<GetEtebarDorehResponse>;
