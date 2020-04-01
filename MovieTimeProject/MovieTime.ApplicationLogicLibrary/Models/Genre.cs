using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class Genre
    {
        [Key]
        public int IdGenre { get; set; }
        public string GenreName { get; set; }
        public ICollection<MovieGenre> Movies { get; set; }
    }
}