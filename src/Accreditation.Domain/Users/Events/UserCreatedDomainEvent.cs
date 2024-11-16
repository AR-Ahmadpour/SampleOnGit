using SharedKernel;

namespace Accreditation.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserGUID) : IDomainEvent;
