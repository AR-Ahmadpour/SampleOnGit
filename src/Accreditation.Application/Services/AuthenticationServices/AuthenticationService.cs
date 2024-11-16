using Accrediation.Application.Abstractions;
using Accrediation.Application.Abstractions.Authentication;
using Accrediation.Application.Authentication.Requests.Commands;
using Accrediation.Application.Common.Errors;
using System.Threading;
using System.Threading.Tasks;

namespace Accrediation.Application.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IIdentityService identityService
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _identityService = identityService;
        }

        public async Task<string> Register(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _identityService
                .GetUserByEmail(command.RegisterDto.Username, cancellationToken) is not null)
                throw new DuplicateEmailException();


            if (command.RegisterDto.Password != command.RegisterDto.ConfirmPassword)
                throw new PasswordAndConfirmPasswordMismatchException();

            var result = _identityService.CreateUserAsync(
                command.RegisterDto.Username,
                command.RegisterDto.Password,
                command.RegisterDto.FirstName,
                command.RegisterDto.LastName,
                command.RegisterDto.NationalCode,
                command.RegisterDto.PhoneNumber,
                command.RegisterDto.BirthDate,
                command.RegisterDto.FatherName

                );

            return result.Result.UserId;
        }

        public async Task<string> Login(LoginCommand request)
        {
            var verifiedUser = await _identityService.VerifyUserForLogin(
                request.LoginDto.Username,
                request.LoginDto.Password);

            var claims = await _identityService.GetClaims(verifiedUser!);
            var roles = await _identityService.GetRoles(verifiedUser!);

            var token = _jwtTokenGenerator.GenerateToken(claims, roles, verifiedUser!.Id.ToString());

            return token;
        }
    }
}
