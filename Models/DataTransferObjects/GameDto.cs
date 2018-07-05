using System.ComponentModel.DataAnnotations;

namespace Models.DataTransferObjects
{
    public class GameDto
    {
        private string _title;
        private string _about;

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "title")]
        public string Title
        {
            get => _title;
            set => _title = value.Trim();
        }

        [Required]
        [MinLength(10)]
        [Display(Name = "about")]
        public string About
        {
            get => _about;
            set => _about = value.Trim();
        }
    }
}