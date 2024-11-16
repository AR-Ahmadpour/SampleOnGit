using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Users.Roles.GetRoleUserDetail
{
    public class GetRoleUserDetailHandler
    : IQueryHandler<GetRoleUserDetailQuery, GetRoleUserDetailDto>
    {
        private readonly IUserRepository _userRepository;

        public GetRoleUserDetailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<GetRoleUserDetailDto>> Handle(GetRoleUserDetailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetRoleDetailAsync(request, cancellationToken);
        }
    }
}
