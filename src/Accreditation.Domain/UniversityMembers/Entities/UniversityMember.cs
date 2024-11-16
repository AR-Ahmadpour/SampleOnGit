using Accreditation.Domain.Universites.Entities;
using Accreditation.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Domain.UniversityMembers.Entities
{
    public sealed class UniversityMember//:Entity
    {
        public UniversityMember() { }
        public int Id { get; private set; }
        public RoleUser RoleUser { get; private set; }
        public int RoleUserId { get; private set; }
        public University University { get; private set; }
        public int UniversityId { get; private set; }
        public bool IsActive { get; private set; }

        private UniversityMember(int RoleUserID, int UniversityID,  bool Isactive)
        { 
            RoleUserId=RoleUserID;
            UniversityId = UniversityID;
            IsActive = Isactive;
        }
        public static UniversityMember Create(int RoleUserId, int UniversityId, bool Isactive)
        {
            return new UniversityMember(RoleUserId, UniversityId,  Isactive);
        }
        public void Edit(int UniversityID, bool Isactive)
        {
            UniversityId = UniversityID;
            IsActive = Isactive;
        }
    }
}
