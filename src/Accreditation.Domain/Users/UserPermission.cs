using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Domain.Users
{
    public sealed class UserPermission
    {
        public int Id { get; set; }
        public Guid UserGUID { get; set; }
        public int PermissionId { get; set; }
        public Guid CreateByGUID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateByGUID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsAllowed { get; set; }

        public Permission Permission { get; set; }
        public User User { get; set; }
    }
}
