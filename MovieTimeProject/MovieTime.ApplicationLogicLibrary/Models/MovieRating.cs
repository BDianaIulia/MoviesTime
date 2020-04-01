using System.ComponentModel.DataAnnotations;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class MovieRating
    {
        [Key]
        public int IdMovieRating { get; set; }
        public int NumberOf5ReviewStars { get; set; }
        public int NumberOf4ReviewStars { get; set; }
        public int NumberOf3ReviewStars { get; set; }
        public int NumberOf2ReviewStars { get; set; }
        public int NumberOf1ReviewStars { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}