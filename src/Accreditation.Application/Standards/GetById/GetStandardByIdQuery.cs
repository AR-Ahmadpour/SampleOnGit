

using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Standards.GetById
{
    public sealed record GetStandardByIdQuery(Guid GUID) : IQuery<GetStandardResponse>;
}
