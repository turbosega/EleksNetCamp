using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects
{
    public class ScoreDto
    {
        [Required]
        [Display(Name = "userId")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "gameId")]
        public int GameId { get; set; }

        [Required]
        [Display(Name = "result")]
        public int Result { get; set; }
    }
}