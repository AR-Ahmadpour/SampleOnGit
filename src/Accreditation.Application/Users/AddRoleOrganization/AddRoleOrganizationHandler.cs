using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.OrganizationMembers;
using Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers;
using Accreditation.Application.Users.Roles.GetRoleUserDetail;
using Accreditation.Domain.OrganizationMembers.Entities;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.AddRoleOrganization
{
    public sealed class AddRoleOrganizationHandler
     : ICommandHandler<AddRoleOrganizationCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationMemberRepository _OrganizationMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleOrganizationHandler(IUserRepository userRepository, IOrganizationMemberRepository organizationMemberRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _OrganizationMemberRepository = organizationMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddRoleOrganizationCommand Command, CancellationToken cancellationToken)
        {
            try
            {
                var getRoleUserDetailQuery = new GetRoleUserDetailQuery(Command.RolesId, Command.UsersGUID);
                var RoleDetail = await _userRepository.GetRoleDetailAsync(getRoleUserDetailQuery);
                if (RoleDetail != null)
                {
                    return Result.Failure<bool>(UserErrors.DuplicateAddRoleUser);
                }
                await _unitOfWork.BeginTransactionAsync();

                var roleUser = RoleUser.Create(Command.RolesId, Command.UsersGUID, Command.CreateByGUID, true);
                _userRepository.AddRoleUser(roleUser);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var Organizationmember = OrganizationMember.Create(roleUser.Id, Command.OrganizationID);
                _OrganizationMemberRepository.Add(Organizationmember);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
                return Result.Success(true);

            }
            catch
            {
                _unitOfWork.Rollback();
                return Result.Failure<bool>(UserErrors.ErrorAddRoleOrganization);
            }
        }
    }
}
