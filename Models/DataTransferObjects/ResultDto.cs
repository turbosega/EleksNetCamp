using System.ComponentModel.DataAnnotations;

namespace Models.DataTransferObjects
{
    public class ResultDto
    {
        [Required]
        [Display(Name = "userId")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "gameId")]
        public int GameId { get; set; }

        [Required]
        [Display(Name = "score")]
        public int Score { get; set; }
    }
}