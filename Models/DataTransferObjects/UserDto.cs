using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.DataTransferObjects
{
    public class UserDto
    {
        private string _login;

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "login")]
        public string Login
        {
            get => _login;
            set => _login = value.Trim();
        }

        [Required]
        [MinLength(8)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "avatar")]
        public IFormFile Avatar { get; set; }
    }
}