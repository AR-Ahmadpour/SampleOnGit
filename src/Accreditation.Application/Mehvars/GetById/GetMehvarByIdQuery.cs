using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Mehvars.GetById;

public sealed record GetMehvarByIdQuery(Guid GUID) : IQuery<GetMehvarResponse>;

