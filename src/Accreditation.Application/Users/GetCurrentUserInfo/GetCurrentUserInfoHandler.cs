using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetCurrentUserInfo
{
    internal class GetCurrentUserInfoHandler
     :IQueryHandler<GetCurrentUserInfoQuery ,GetCurrentUserInfoQueryDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetCurrentUserInfoHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCurrentUserInfoQueryDto>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
           return await _userRepository.GetCurrentUserInfo(request,cancellationToken);
        }
    }
}
