using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using Serilog.Parsing;
using SharedKernel;

namespace Accreditation.Application.Users.LogInUser;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, LogInUserDto>
{
    //private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    public LogInUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<LogInUserDto>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        //Result<string> result = await _jwtService.GetAccessTokenAsync(
        //    request.username,
        //    request.Password,
        //    cancellationToken);

        Result<LogInUserDto> result = await _userRepository.LoginAsync(request.username, request.Password, cancellationToken);
        if (result.IsSuccess == false)
        {
            return Result.Failure<LogInUserDto>(Domain.Users.UserErrors.InvalidCredentials);
        }

        return result;// AccessTokenResponse(result.Value);
    }
}
