using System.Collections.Generic;
using Models.Enumerations;
using Newtonsoft.Json;

namespace Models.Entities
{
    public class User : BaseEntity
    {
        public string Login     { get; set; }
        public string AvatarUrl { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public UserType UserType { get; set; } = UserType.RegularUser;

        public virtual IEnumerable<Result> Results { get; set; }

        public User() => Results = new List<Result>();
    }
}