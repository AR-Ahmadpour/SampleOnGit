using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Sanjehs.GetById;

namespace Accreditation.Application.AccreditationInstances.GetById;
public sealed record GetAccreditationalInstanceByIdQuery( Guid AccreditationInstanceId) :
    IQuery<GetAccreditationalInstanceByIdDto>;
