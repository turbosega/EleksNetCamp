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
        [Display(Name = "gameOutcome")]
        public string GameOutcome { get; set; }
    }
}