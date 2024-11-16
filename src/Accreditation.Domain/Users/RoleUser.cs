using Accreditation.Domain.OrganizationMembers.Entities;
using Accreditation.Domain.Universites.Entities;
using Accreditation.Domain.UniversityMembers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Domain.Users
{
    public sealed class RoleUser
    {
        public RoleUser()
        {
            //Roles = new List<Role>();
            //Users = new List<User>();
        }
        public int Id { get; set; }
        public int RolesId { get; set; }
        public Guid UsersGUID { get; set; }
        public Guid CreateByGUID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateByGUID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        //public ICollection<Role> Roles {  get; set; }
        //public ICollection<User> Users { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
        public UniversityMember? UniversityMember { get; set; }
        public OrganizationMember? organizationMember { get; set; }
        //public  University? University { get; set; }

        private RoleUser(int RoleId  , Guid UserGUID, Guid CreateByGuid ,  bool Isactive)
        { 
            RolesId= RoleId;
            UsersGUID = UserGUID;
            CreateByGUID= CreateByGuid;
            CreateDate = DateTime.Now;
            IsActive= Isactive;
        }

        public static RoleUser Create(int RolesID, Guid UserGUID, Guid CreateByGuid,  bool Isactive)
        {
            return new RoleUser(RolesID, UserGUID, CreateByGuid,  Isactive);
        }

        public void  Edit(int RoleId, Guid UserGUID, Guid UpdateByGuid, bool Isactive)
        {
            RolesId = RoleId;
            UsersGUID = UserGUID;                        
            UpdateByGUID = UpdateByGuid;
            UpdateDate= DateTime.Now;
            IsActive = Isactive;
        }
    }
}
