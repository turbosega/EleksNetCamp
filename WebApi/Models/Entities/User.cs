using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApi.Models.Entities
{
    public class User
    {
        public int    Id    { get; set; }
        public string Login { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        private IEnumerable<Score> _scores;

        public virtual IEnumerable<Score> Scores
        {
            get => _scores;
            set => _scores = value;
        }

        public User() => _scores = new List<Score>();
    }
}