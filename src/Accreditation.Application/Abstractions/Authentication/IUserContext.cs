using Accreditation.Domain.Users;

namespace Accreditation.Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid UserGUId { get; }

    string Role { get; }
}
