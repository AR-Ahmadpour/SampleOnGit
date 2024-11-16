namespace Accreditation.Application.Services.UserServices.Dtos
{
    public class GetAllUsersInfoDto
    {
        public GetAllUsersInfoDto()
        {
            Roles = new List<string>();
        }

        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Fathername { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }
    }
}
