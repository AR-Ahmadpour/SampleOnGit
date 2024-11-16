using Accreditation.Domain.OrganizationMembers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Persistence.OrganizationMembers
{
    public interface IOrganizationMemberRepository
    {
        void Add(OrganizationMember organizationMember);
        Task<OrganizationMember?> Find(int RoleUserID);
    }
}
