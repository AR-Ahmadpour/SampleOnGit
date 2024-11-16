using SharedKernel;

namespace Accreditation.Domain.EtebarDorehs.Events
{
    public sealed record EtebarDorehDomainEvent( Guid Guid) : IDomainEvent;

}
