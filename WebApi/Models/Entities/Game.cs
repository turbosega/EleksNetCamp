using System;
using System.Collections.Generic;
using WebApi.Extensions;

namespace WebApi.Models.Entities
{
    public class Game
    {
        public int    Id    { get; set; }
        public string Title { get; set; }

        private IEnumerable<Score> _scores;

        public IEnumerable<Score> Scores
        {
            get => LazyLoader.Load(this, ref _scores);
            set => _scores = value;
        }

        public Game() => _scores = new List<Score>();

        private Action<object, string> LazyLoader { get; set; }

        private Game(Action<object, string> lazyLoader) => LazyLoader = lazyLoader;
    }
}