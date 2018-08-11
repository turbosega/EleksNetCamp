using System.Collections.Generic;

namespace Models.Entities
{
    public class Game : BaseEntity<int>
    {
        public string Title    { get; set; }
        public string About    { get; set; }
        public string ImageSrc { get; set; }

        private IEnumerable<Result> _results;

        public virtual IEnumerable<Result> Results
        {
            get => _results;
            set => _results = value;
        }

        public Game() => _results = new List<Result>();
    }
}