using Accreditation.Domain.Users;

namespace Accreditation.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password, 
        CancellationToken cancellationToken = default);
}
