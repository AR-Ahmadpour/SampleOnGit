using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.EditRoleUser
{
    internal sealed class EditRoleUserHandler : ICommandHandler<EditRoleUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUniversityMemberRepository _universityMemberRepository;
        
        private readonly IUnitOfWork _unitOfWork;

        public EditRoleUserHandler(IUserRepository userRepository, IUniversityMemberRepository universityMemberRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _universityMemberRepository = universityMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(EditRoleUserCommand request, CancellationToken cancellationToken)
        {
            var roleUser =await  _userRepository.RoleUserFindAsync(request.RoleUserId, cancellationToken);
            if(roleUser == null)
            {
                return Result.Failure<int>(UserErrors.ErrorEditRoleUser);
            }
            var universityMember = await _universityMemberRepository.Find(roleUser.Id);
            if (universityMember == null)
            {
                return Result.Failure<int>(UserErrors.ErroNotFoundRoleUniversity);
            }
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                roleUser.Edit(request.RoleId, request.UsersGUID, request.UpdateByGUID, request.IsActive);              
                universityMember.Edit(request.UniversityID, request.IsActive);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Result.Failure<int>(UserErrors.ErroEditRoleUniversity);                
            }
            return roleUser.Id;
        }
    }
}
