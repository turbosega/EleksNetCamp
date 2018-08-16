namespace Models.DataTransferObjects
{
    public class UserWithRatingDto
    {
        public int    Id          { get; set; }
        public string Login       { get; set; }
        public string AvatarUrl   { get; set; }
        public double RatingScore { get; set; }
    }
}