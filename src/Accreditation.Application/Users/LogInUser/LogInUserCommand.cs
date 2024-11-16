using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Services.AuthenticationServices.Dtos;

namespace Accreditation.Application.Users.LogInUser;

public sealed record LogInUserCommand(string username, string Password)
    : ICommand<LogInUserDto>;
