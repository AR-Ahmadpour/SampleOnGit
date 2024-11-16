
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Users.ToggleUserPermission
{
    internal class ToggleUserPermissionHandler
   :ICommandHandler<ToggleUserPermissionCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleUserPermissionHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ToggleUserPermissionCommand command, CancellationToken cancellationToken)
        {
            var userp = await _userRepository.UserPermissionEsixt(command.UserPermissionID);
            if (userp == null)
            {
                return Result.Failure(UserErrors.UserNotFoundUserPermission);
            }
            userp.IsAllowed =! userp.IsAllowed;
            userp.UpdateDate = DateTime.Now;
            userp.UpdateByGUID = command.CurrentUserId;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
