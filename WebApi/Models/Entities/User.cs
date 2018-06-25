using Newtonsoft.Json;

namespace WebApi.Models.Entities
{
    public class User
    {
        public int    Id    { get; set; }
        public string Login { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}