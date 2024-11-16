using MediatR;
using Accrediation.Application.Common.Models;
using Accrediation.Application.Services.AuthenticationServices.Dtos;
using Accrediation.Application.Services.UserServices;
using Accrediation.Application.Services.UserServices.Dtos;
using Accrediation.Application.Users.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accrediation.Application.Users.Handlers.Queries
{
    public class GetUserShowInfoHandher : IRequestHandler<GetUserShowInfoQuery, GetUserInfoDto>
    {
        private readonly IUserService _userService;

        public GetUserShowInfoHandher(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserInfoDto> Handle(
        GetUserShowInfoQuery request,
        CancellationToken cancellationToken)
        {
            return await _userService.GetUserShowInfo(request, cancellationToken);
        }
    }
}
