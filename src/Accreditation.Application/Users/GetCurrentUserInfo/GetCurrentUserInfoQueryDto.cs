using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetCurrentUserInfo
{
    public sealed record  GetCurrentUserInfoQueryDto
    {
        public  Guid     Userid              { get; set; }
        public string?   Username            { get; set; }
        public string?   FirstName           { get; set; }
        public string?   LastName            { get; set; }
        public string?   FatherName          { get; set; }
        public string?   NationalCode        { get; set; }
        public string?   BirthDate           { get; set; }   
        public string?   Email               { get; set; }
        public string?   PhoneNumber         { get; set; }
        public string?   Ostan               { get; set; }
        public string?   City                { get; set; }
        public string?   Address             { get; set; }
        public string?   OrganizationType    { get; set; }
        public string?   Organization        { get; set; }
        public string?   Univercity          {  get; set; } 
    }
}
