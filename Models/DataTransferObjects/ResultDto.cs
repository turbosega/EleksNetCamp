namespace Models.DataTransferObjects
{
    public class ResultDto
    {
        public int Id    { get; set; }
        public int Score { get; set; }

        public int    UserId        { get; set; }
        public string UserLogin     { get; set; }
        public string UserAvatarUrl { get; set; }

        public int    GameId    { get; set; }
        public string GameTitle { get; set; }
    }
}