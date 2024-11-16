using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.AddPermission;
using Accreditation.Domain.Users;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.AddUserPermission
{
    internal class AddUserPermissionHandler
        :ICommandHandler<AddUserPermissionCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserPermissionHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddUserPermissionCommand request, CancellationToken cancellationToken)
        {

                var userPermission =await _userRepository.UserPermissionFindAync(request.UserGUID, request.PermissionId);
                if (userPermission == false)
                {
                    _userRepository.AddUserPermission(request, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Result.Success();
                }
                else
                {
                    return Result.Failure<Result>(UserErrors.DuplicateUserPermission);
                }
            
        }

         
    }
}
