using System.Collections.Generic;
using Models.Enumerations;

namespace Models.Entities
{
    public class User : BaseEntity<int>
    {
        public string Login     { get; set; }
        public string AvatarUrl { get; set; }

        public string PasswordHash { get; set; }

        public UserType UserType { get; set; } = UserType.RegularUser;

        public virtual IEnumerable<Result> Results { get; set; }

        public User() => Results = new List<Result>();
    }
}