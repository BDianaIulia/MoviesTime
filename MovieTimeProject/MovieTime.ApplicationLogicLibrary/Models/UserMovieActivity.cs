using System;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class UserMovieActivity
    {
        public string Status { get; set; }

        public Guid IdUser { get; set; }
        public User User { get; set; }
        public Guid IdMovie { get; set; }
        public Movie Movie { get; set; }
    }
}