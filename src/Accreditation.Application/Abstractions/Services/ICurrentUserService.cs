namespace Accreditation.Application.Abstractions.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Roles { get; }
    string[]? Permissions { get; }


}
