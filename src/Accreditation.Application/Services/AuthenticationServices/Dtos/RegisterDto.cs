using System.ComponentModel.DataAnnotations;

namespace Accreditation.Application.Services.AuthenticationServices.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MinLength(2)]  
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string LastName { get; set; }


        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [EmailAddress]
        public string Username { get; set; }


        public string? FatherName { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "کد ملی 10 رقم باید باشد")]
        public string NationalCode { get; set; }

        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
