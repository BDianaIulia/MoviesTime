using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string GenreName { get; set; }
        public ICollection<MovieGenre> Movies { get; set; }
    }
}