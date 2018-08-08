using System.ComponentModel.DataAnnotations;

namespace Models.DataTransferObjects.Creating
{
    public class GameCreatingDto
    {
        private string _title;
        private string _about;
        private string _imageSrc;

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

        [Required]
        [Display(Name = "imageSrc")]
        public string ImageSrc
        {
            get => _imageSrc;
            set => _imageSrc = value.Trim();
        }
    }
}