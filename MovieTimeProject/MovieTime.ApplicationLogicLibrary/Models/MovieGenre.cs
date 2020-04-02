using System;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class MovieGenre
    {
        public Guid IdMovie { get; set; }
        public Movie Movie { get; set; }
        public Guid IdGenre { get; set; }
        public Genre Genre { get; set; }
    }
}