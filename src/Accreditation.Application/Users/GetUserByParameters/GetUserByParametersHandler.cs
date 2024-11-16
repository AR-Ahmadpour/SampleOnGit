using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.GetByParameters;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetUserByParameters
{
    internal sealed class GetUserByParametersHandler
        :IQueryHandler<GetUserByParametersQuery, List<GetUserByParametersDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByParametersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 

        public async Task<Result<List<GetUserByParametersDto>>> Handle(GetUserByParametersQuery request, CancellationToken cancellationToken)
        {

            if(request.SearchParam == null || request.SearchParam ==""  ) 
            {
                return Result.Failure<List<GetUserByParametersDto>>(UserErrors.GetUserByParameters);
            }
            return await _userRepository.GetUserByParameters(request, cancellationToken);

        }
    }
}
