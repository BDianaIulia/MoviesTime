﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string UserName { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserMovieActivity> RelatedListMovies { get; set; }
    }
}