using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects
{
    public class GameDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "title")]
        public string Title { get; set; }
    }
}