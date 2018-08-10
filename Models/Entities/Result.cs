namespace Models.Entities
{
    public class Result : BaseEntity<int>
    {
        public int Score { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}