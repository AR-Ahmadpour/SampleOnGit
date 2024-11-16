using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.GetAccessToken;
using Accreditation.Application.Users.LogInUser;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetSelectedRole
{
    public class GetPermissionByUserHandler
    :IQueryHandler<GetPermissionByUserQuery , AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public GetPermissionByUserHandler(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;

        }

        public async Task<Result<AccessTokenResponse>> Handle(GetPermissionByUserQuery request, CancellationToken cancellationToken)
        {
            var res = await _userRepository.GetPermissionByUser(request, cancellationToken);

            Result<string> result = await _jwtService.GetAccessTokenAsync(
                request,
                //request.username,
                //request.Password,
                cancellationToken);            
            return new AccessTokenResponse(result.Value);

        }
    }
}
