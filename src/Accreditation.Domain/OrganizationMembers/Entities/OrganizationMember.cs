using Accreditation.Domain.Users;
using Accreditation.Domain.Organizations.Entities;
using SharedKernel;
using System;
using System.Text;

namespace Accreditation.Domain.OrganizationMembers.Entities
{
    public sealed class OrganizationMember
    {
        public int Id { get; private set; }
        public RoleUser RoleUser { get; private set; }
        public int RoleUserId { get; private set; }
        public Guid OrganizationGUID { get; private set; }
        //public bool IsChieff { get; private set; }
        public bool IsActive { get; private set; }
        public Organizations.Entities.Organization Organization { get; set; }

        public OrganizationMember() { }

        private OrganizationMember(int RoleUserID, Guid OrganizationGuid) 
        { 
            RoleUserId = RoleUserID;
            OrganizationGUID= OrganizationGuid;
            IsActive = true;
            //IsChieff = false;
        }
        
        public static OrganizationMember Create(int RoleUserID, Guid OrganizationGuid)
        {
            return new OrganizationMember(RoleUserID, OrganizationGuid);
        }
        public void Edit(Guid OrganizationGuid ,bool Isactive)
        {
            OrganizationGUID = OrganizationGuid;
            IsActive = Isactive;
        }
    }
}
