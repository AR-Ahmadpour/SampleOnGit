using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.UserToggleState;
using Accreditation.Domain.Users;
using SharedKernel;


namespace Accreditation.Application.Users.PersonToggleState
{
    internal class UserToggleStateHandler
        :ICommandHandler<UserToggleStateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserToggleStateHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UserToggleStateCommand request, CancellationToken cancellationToken)
        {

                var user = await _userRepository.GetByIdAsync(request.Uid, cancellationToken);
                if (user is null)
                {
                    return Result.Failure<Guid>(UserErrors.UserNotFound);
                }
                bool LogicalDelete = user.IsDeleted ?? false;
                user.LogicalDelete(LogicalDelete);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();            
        }
    }
}
