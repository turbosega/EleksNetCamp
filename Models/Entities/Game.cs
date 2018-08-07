using System.Collections.Generic;

namespace Models.Entities
{
    public class Game : BaseEntity
    {
        public string Title    { get; set; }
        public string About    { get; set; }
        public string ImageSrc { get; set; }

        public virtual IEnumerable<Result> Results { get; set; }

        public Game() => Results = new List<Result>();
    }
}