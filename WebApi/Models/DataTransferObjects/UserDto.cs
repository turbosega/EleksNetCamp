using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects
{
    public class UserDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}