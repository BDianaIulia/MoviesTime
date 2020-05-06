using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserMovieActivity> RelatedListMovies { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        public string ErrorLogin { get; set; }
    }
}