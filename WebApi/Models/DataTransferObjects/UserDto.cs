using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects
{
    public class UserDto
    {
        private string _name;

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "login")]
        public string Login
        {
            get => _name;
            set => _name = value.Trim();
        }

        [Required]
        [MinLength(8)]
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}