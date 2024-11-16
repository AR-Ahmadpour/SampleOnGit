using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;


namespace Accreditation.Application.Users.Roles.GetRoleByIsSetadi
{
    public class GetRoleByIsSetadiHandler
        : IQueryHandler<GetRoleByIsSetadiQuery, List<GetRoleByIsSetadiDto>>
    {

        private readonly IUserRepository _userRepository;
        public GetRoleByIsSetadiHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<GetRoleByIsSetadiDto>>> Handle(GetRoleByIsSetadiQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetRoleByIsSetadiAsync(query.IsSetadi, cancellationToken);
        }
    }
}
