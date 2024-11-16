using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Headers.GetList;

namespace Accreditation.Application.Headers.GetBy
{
    public sealed record GetHeaderQuery(Guid FieldGuid, Guid AccreditationInstanceGuid):
        IQuery<GetHeaderDto>;
}
