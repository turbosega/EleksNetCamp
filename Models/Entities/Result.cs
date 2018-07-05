using System;
using Models.Enumerations;

namespace Models.Entities
{
    public class Result
    {
        public int         Id          { get; set; }
        public GameOutcome GameOutcome { get; set; }
        public DateTime    DateTime    { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}