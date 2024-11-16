using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetAllRoleUserOrganization
{
    public sealed record GetAllRoleUserOrganizationDto
    {
        public string? NationalCode { get; init; }
        public string FullName { get; init; }
        public int RoleId { get; init; }
        public Guid UserId { get; init; }
        public int RoleUserId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string? OrganizationTitel { get; init; }
        public Guid? OrganizationGuid { get; init; }

        public int? OrganizationMemberId { get; init; }
        /// <summary>
        /// حذف منطقی دسترسی
        /// </summary>
        public bool IsActive { get; init; }
    }
}
