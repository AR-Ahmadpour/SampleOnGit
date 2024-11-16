using System.ComponentModel.DataAnnotations;

namespace Accrediation.Application.Services.DocumentServices.Dtos
{
    public class UploadFileDto
    {
        [Required]
        public byte[] Data { get; set; }

        [Required]
        [MaxLength(65)]
        public string Extension { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
