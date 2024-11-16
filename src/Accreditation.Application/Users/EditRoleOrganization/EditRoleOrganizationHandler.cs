using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.OrganizationMembers;
using Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers;
using Accreditation.Domain.Users;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.EditRoleOrganization
{
    internal sealed class EditRoleOrganizationHandler
        :ICommandHandler<EditRoleOrganizationCommand ,int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationMemberRepository _organizationMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditRoleOrganizationHandler(
            IUserRepository userRepository, 
            IOrganizationMemberRepository organizationMemberRepository, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _organizationMemberRepository = organizationMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(EditRoleOrganizationCommand command, CancellationToken cancellationToken)
        {

            var roleUser = await _userRepository.RoleUserFindAsync(command.RoleUserId, cancellationToken);
            if (roleUser == null)
            {
                return Result.Failure<int>(UserErrors.ErrorEditRoleUser);
            }
            var organizationMember = await _organizationMemberRepository.Find(roleUser.Id);
            if (organizationMember == null)
            {
                return Result.Failure<int>(UserErrors.ErroEditRoleOrganization);
            }
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                roleUser.Edit(command.RoleId, command.UsersGUID, command.UpdateByGUID, command.IsActive);
                organizationMember.Edit(command.OrganizationID, command.IsActive);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Result.Failure<int>(UserErrors.ErroEditRoleOrganization);
            }
            return roleUser.Id;
        }

    }
}
