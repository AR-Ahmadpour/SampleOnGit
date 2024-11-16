

using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.ZirMehvars.GetById;

public sealed record GetZirMehvarByIdQuery(Guid GUID) : IQuery<GetZirMehvarResponse>;
