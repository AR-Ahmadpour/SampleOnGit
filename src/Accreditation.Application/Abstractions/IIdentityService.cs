using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Abstractions;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId, CancellationToken cancellationToken);

    Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken);

    Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellationToken);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string Firstname, string Lastname, string Nationalcode, string Phonenumber, DateTime Birthdate, string Fathername);

    Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken);

    // Task<User> VerifyUserForLogin(string username, string password);

     Task<User?> FindById(string userId);

    // Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);

    // Task<IList<string>> GetRoles(User user);

    //Task<IList<Claim>> GetClaims(User user);

    Task<bool> DoesUserExist(string userId, CancellationToken cancellationToken);

    Task<bool> DoesEmailExist(string email, CancellationToken cancellationToken);

    // Task AddToRole(User user, string role);
}
