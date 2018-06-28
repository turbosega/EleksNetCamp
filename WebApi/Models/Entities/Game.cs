using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

namespace WebApi.Models.Entities
{
    public class Game
    {
        public int    Id        { get; set; }
        public string Title     { get; set; }

        private IEnumerable<Score> _scores;

        [JsonIgnore]
        private ILazyLoader LazyLoader { get; set; }

        public virtual IEnumerable<Score> Scores
        {
            get => _scores;
            set => _scores = value;
        }

        public Game() => _scores = new List<Score>();
    }
}