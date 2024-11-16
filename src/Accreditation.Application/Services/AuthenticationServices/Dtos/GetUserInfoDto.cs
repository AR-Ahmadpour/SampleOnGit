namespace Accreditation.Application.Services.AuthenticationServices.Dtos
{
    public class GetUserInfoDto
    {
        public GetUserInfoDto()
        {
            Roles = new List<string>();
            Categories = new List<string>();
        }

        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? HelpDeskTitle { get; set; }
        public Guid? HelpDeskId { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public string? DepartmentTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string? FatherName { get; set; }
        public Guid? DepartmentId { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Categories { get; set; }
        public Guid? HelpDeskAvatarId { get; set; }
        public string? HelpDeskAvatarExtension { get; set; }
        //public Guid? DepartmentAvatarId { get; set; }
        public byte[] DepartmentAvatarLogo { get; set; }
        public string? DepartmentAvatarExtension { get; set; }
    }
}
