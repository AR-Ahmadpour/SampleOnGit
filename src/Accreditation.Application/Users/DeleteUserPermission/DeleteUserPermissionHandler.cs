using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.DeleteUserPermission
{
    internal class DeleteUserPermissionHandler
        :ICommandHandler<DeleteUserPermissionCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserPermissionHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteUserPermissionCommand command , CancellationToken cancellationToken)
        {
            var userp = await _userRepository.UserPermissionEsixt(command.UserPermissionID);
            if (userp == null)
            {
                return Result.Failure(UserErrors.UserNotFoundUserPermission);
            }
            _userRepository.DeleteUserPermission(userp);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
