using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetUserPermissionByUserID
{
    internal class GetUserPermissionByUserIDHandler
        :IQueryHandler<GetUserPermissionByUserIDQuery,List<GetUserPermissionByUserIdDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserPermissionByUserIDHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<GetUserPermissionByUserIdDto>>> Handle(GetUserPermissionByUserIDQuery query,CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserPermissionByUserId(query, cancellationToken);
        }
    }
}
