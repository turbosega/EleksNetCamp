using System.ComponentModel.DataAnnotations;

namespace Models.DataTransferObjects.Creating
{
    public class ResultCreatingDto
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