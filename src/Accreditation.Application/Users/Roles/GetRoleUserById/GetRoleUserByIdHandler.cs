using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetRoleUserById
{
    public class GetRoleUserByIdHandler
        :IQueryHandler<GetRoleUserByIdQuery ,PagedList<GetRoleUserByIdDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetRoleUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<GetRoleUserByIdDto>>> Handle(GetRoleUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetRoleUserByIdAsync(request, cancellationToken);
        }
    }
}
