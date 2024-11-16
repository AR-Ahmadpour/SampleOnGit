using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Users;
using SharedKernel;


namespace Accreditation.Application.Users.Roles.GetAllRoleUser
{
    public class GetAllRoleUserHandler
     : IQueryHandler<GetAllRoleUserQuery, PagedList<GetAllRoleUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllRoleUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<GetAllRoleUserDto>>> Handle(GetAllRoleUserQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllRoleUserAsync(query, cancellationToken);
        }
    }
}