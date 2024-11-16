using Accreditation.Domain.UniversityMembers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Persistence.UniversityMembers
{
    public interface IUniversityMemberRepository
    {
        void Add(UniversityMember universityMember);
        Task<UniversityMember> Find(int RoleUserId);
    }
}
