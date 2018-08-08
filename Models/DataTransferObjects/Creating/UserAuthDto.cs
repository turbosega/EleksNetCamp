using System.ComponentModel.DataAnnotations;

namespace Models.DataTransferObjects.Creating
{
    public class UserAuthDto
    {
        private string _login;

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "login")]
        public string Login {
            get => _login;
            set => _login = value.Trim();
        }

        [Required]
        [MinLength(8)]
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}