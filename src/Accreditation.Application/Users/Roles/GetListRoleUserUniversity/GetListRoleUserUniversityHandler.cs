using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.GetAccessToken;
using Accreditation.Application.Users.LogInUser;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetListRoleUserUniversity
{
    public class GetListRoleUserUniversityHandler
        :IQueryHandler<GetListRoleUserUniversityQuery, PagedList<GetListRoleUserUniversityDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetListRoleUserUniversityHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<GetListRoleUserUniversityDto>>> Handle(
            GetListRoleUserUniversityQuery request, 
            CancellationToken cancellationToken)
        {
            if (DateTime.Parse(request.DateNow) < DateTime.Now.AddMinutes(-5))
            {
                return Result.Failure<PagedList<GetListRoleUserUniversityDto>>(Domain.Users.UserErrors.ExpireToken);
            }

            Result<PagedList<GetListRoleUserUniversityDto>> result = await _userRepository.GetListRoleUserUniversityAsync(request, cancellationToken);
            if (result.IsSuccess == false || result.Value.Items.Count==0)
            {
                return Result.Failure<PagedList<GetListRoleUserUniversityDto>>(Domain.Users.UserErrors.ListRoleUserUniversity);
            }

            return result;
        }
    }
}
