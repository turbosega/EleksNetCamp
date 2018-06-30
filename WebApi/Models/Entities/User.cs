using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Extensions;

namespace WebApi.Models.Entities
{
    public class User
    {
        public int    Id    { get; set; }
        public string Login { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        private IEnumerable<Score> _scores;

        public IEnumerable<Score> Scores
        {
            get => LazyLoader.Load(this, ref _scores);
            set => _scores = value;
        }

        public User() => _scores = new List<Score>();

        private Action<object, string> LazyLoader { get; set; }

        private User(Action<object, string> lazyLoader) => LazyLoader = lazyLoader;
    }
}