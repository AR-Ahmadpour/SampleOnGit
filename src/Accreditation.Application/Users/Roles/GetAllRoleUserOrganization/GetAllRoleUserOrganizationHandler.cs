using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.Roles.GetAllRoleUser;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetAllRoleUserOrganization
{
    internal class GetAllRoleUserOrganizationHandler
       : IQueryHandler<GetAllRoleUserOrganizationQuery, PagedList<GetAllRoleUserOrganizationDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllRoleUserOrganizationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<GetAllRoleUserOrganizationDto>>> Handle(GetAllRoleUserOrganizationQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllRoleUserOrgAsync(query, cancellationToken);
        }
    }
}
