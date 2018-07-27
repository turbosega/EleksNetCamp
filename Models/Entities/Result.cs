using System;
using Newtonsoft.Json;

namespace Models.Entities
{
    public class Result
    {
        public int Id    { get; set; }
        public int Score { get; set; }

        [JsonIgnore]
        public DateTime DateTime { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}