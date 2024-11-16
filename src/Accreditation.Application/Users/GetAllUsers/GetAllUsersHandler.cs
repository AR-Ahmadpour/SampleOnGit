using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetAllPersons
{
    internal class GetAllUsersHandler
        :IQueryHandler<GetAllUsersQuery ,PagedList<GetAllUsersDto>>
    {
        private readonly IUserRepository  _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<GetAllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
           return await  _userRepository.GetAllUsers(request, cancellationToken);
        }
    }
}
