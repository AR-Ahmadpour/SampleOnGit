using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Permissions;
using Accreditation.Domain.Users;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetPermisionByCategory
{
    internal class GetPermisionByCategoryHandler:
    IQueryHandler<GetPermisionByCategoryQuery, List<GetPermisionByCategoryDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetPermisionByCategoryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<GetPermisionByCategoryDto>>> Handle(GetPermisionByCategoryQuery query  , CancellationToken cancellationToken)
        {
            return await _userRepository.GetPermisionByCategory(query, cancellationToken);
        }
    
    }
}
