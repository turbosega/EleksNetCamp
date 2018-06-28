using Newtonsoft.Json;

namespace WebApi.Models.Entities
{
    public class Score
    {
        public int Id     { get; set; }
        public int Result { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Game Game { get; set; }
    }
}