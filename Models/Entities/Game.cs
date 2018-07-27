using System.Collections.Generic;

namespace Models.Entities
{
    public class Game
    {
        public int    Id       { get; set; }
        public string Title    { get; set; }
        public string About    { get; set; }
        public string ImageSrc { get; set; }

        public virtual IEnumerable<Result> Results { get; set; }

        public Game() => Results = new List<Result>();
    }
}