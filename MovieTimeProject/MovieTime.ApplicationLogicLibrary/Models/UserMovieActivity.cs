namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class UserMovieActivity
    {
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}