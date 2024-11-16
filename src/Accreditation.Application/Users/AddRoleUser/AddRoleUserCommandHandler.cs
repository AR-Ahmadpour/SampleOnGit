using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers;
using Accreditation.Application.Users.Roles.GetRoleUserDetail;
using Accreditation.Domain.UniversityMembers.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Users.AddRoleUser
{
    internal sealed class AddRoleUserCommandHandler : ICommandHandler<AddRoleUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUniversityMemberRepository _universityMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleUserCommandHandler(
            IUserRepository userRepository,
            IUniversityMemberRepository universityMemberRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _universityMemberRepository = universityMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle
        (AddRoleUserCommand Command, CancellationToken cancellationToken)
        {
            try
            {
                var getRoleUserDetailQuery = new GetRoleUserDetailQuery(Command.RolesId,Command.UsersGUID);
                var RoleDetail =await _userRepository.GetRoleDetailAsync(getRoleUserDetailQuery);
                if(RoleDetail != null)
                {
                    return Result.Failure<bool>(UserErrors.DuplicateAddRoleUser);
                }
                await _unitOfWork.BeginTransactionAsync();
                
                var roleUser = RoleUser.Create(Command.RolesId,Command.UsersGUID, Command.CreateByGUID, true);                
                _userRepository.AddRoleUser(roleUser);
                await _unitOfWork.SaveChangesAsync(cancellationToken);


                var universityMember = UniversityMember.Create(roleUser.Id, Command.UniversityID, true);
                _universityMemberRepository.Add(universityMember);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
                return Result.Success(true);

            }
            catch
            {
                _unitOfWork.Rollback();
                return Result.Failure<bool>(UserErrors.ErrorAddRoleUser);
            }
        }
    }
}
