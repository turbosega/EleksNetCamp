using System.Collections.Generic;

namespace WebApi.Models.Entities
{
    public class Game
    {
        public int    Id    { get; set; }
        public string Title { get; set; }

        public virtual IEnumerable<Score> Scores { get; set; }
    }
}