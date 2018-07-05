using System.Collections.Generic;
using Models.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models.Entities
{
    public class User
    {
        public int    Id           { get; set; }
        public string Login        { get; set; }
        public string PasswordHash { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UserType UserType { get; set; } = UserType.RegularUser;

        public virtual IEnumerable<Result> Results { get; set; }

        public User() => Results = new List<Result>();
    }
}