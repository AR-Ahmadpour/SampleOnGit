using System.ComponentModel.DataAnnotations;

namespace Accreditation.Application.Services.AuthenticationServices.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
