using Accreditation.Application.AccreditationInstances.Add;
using Accreditation.Application.Common.Interfaces.Persistence.OrganizationMembers;
using Accreditation.Domain.OrganizationMembers.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class OrganizationMemberRepository(AccreditationDbContext context): IOrganizationMemberRepository
    {
        public void Add(OrganizationMember organizationMember)
        {
            context.OrganizationMembers.Add(organizationMember);
        }
        public async Task<OrganizationMember?> Find (int RoleUserID ) 
        {
            return await context.OrganizationMembers.
                Where(om => om.RoleUserId == RoleUserID ).FirstOrDefaultAsync();
        }
    }
}
