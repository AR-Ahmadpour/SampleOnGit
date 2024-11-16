using Accrediation.Application.Abstractions;
using Accrediation.Application.Common.Errors.Users;
using Accrediation.Domain.Identity;
using Accrediation.Domain.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Security.Claims;

namespace Accrediation.Infrastructure.Identity;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .Users
            .FirstAsync(u => u.Id.ToString() == userId, cancellationToken);

        return user.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(
        string userName,
        string password,
        string Firstname,
        string Lastname,
        string Nationalcode,
        string Phonenumber,
        DateTime Birthdate,
        string Fathername
        )
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Email = userName,
            EmailConfirmed = false,
            CompletelyRegistered = false,
            IsBlocked = false,
            Gender = Gender.NotDetermined,
            RegisterDate = DateTime.UtcNow,
            FirstName = Firstname,
            LastName = Lastname,
            NationalCode = Nationalcode,
            PhoneNumber = Phonenumber,
            BirthDate = Birthdate,
            FatherName = Fathername
        };

        var result = await _userManager.CreateAsync(user, password);
        return (result.ToApplicationResult(), user.Id.ToString());
    }

    public async Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken);

        return user != null ? await DeleteUserAsync(user.Id.ToString(), cancellationToken) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<User> VerifyUserForLogin(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username);

        if (user == null)
            throw new WrongUsernameOrPasswordException();

        if (!user.EmailConfirmed)
            throw new YourEmailIsNotConfirmedYetException();

        if (user.IsBlocked)
            throw new YourAccountHasBeenBlockedException();

        var passwordVerified = VerifyHashedPassword(user, password);
        if (!passwordVerified)
            throw new WrongUsernameOrPasswordException();

        return user;
    }

    public bool VerifyHashedPassword(User user, string password)
    {
        var passwordVerifiedResult =
            _userManager.PasswordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password);

        if (passwordVerifiedResult == PasswordVerificationResult.Success)
            return true;

        return false;
    }

    public async Task<User?> FindById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new UserNotFoundException();
        return user;
    }

    public async Task<User?> GetUserByEmail(
        string email,
        CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(_ => _.Email == email, cancellationToken);
    }

    public async Task<IList<Claim>> GetClaims(User user)
    {
        return await _userManager.GetClaimsAsync(user);
    }

    public async Task<IList<string>> GetRoles(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> DoesEmailExist(string email, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AnyAsync(_ => _.Email == email, cancellationToken);
    }

    public async Task<bool> DoesUserExist(string userId, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AnyAsync(_ => _.Id.ToString() == userId, cancellationToken);
    }

    public async Task AddToRole(User user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }
}
