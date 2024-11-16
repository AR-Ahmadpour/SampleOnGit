using Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers;
using Accreditation.Domain.Universites.Entities;
using Accreditation.Domain.UniversityMembers.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class UniversityMemberRepository(AccreditationDbContext context):IUniversityMemberRepository
    {    
        public void Add(UniversityMember universityMember)
        {
            context.UniversityMembers.Add(universityMember);
        }

        public async Task<UniversityMember?>  Find(int RoleUserId) 
        {
            return await context.UniversityMembers
                .Where(um =>   um.RoleUserId == RoleUserId).FirstOrDefaultAsync();
        }
    }
}
