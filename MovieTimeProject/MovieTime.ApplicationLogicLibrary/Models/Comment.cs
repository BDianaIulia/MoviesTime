using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public int ReviewScore { get; set; }
        public string CommentText { get; set; }

        public Guid IdMovie { get; set; }
        public Movie Movie { get; set; }

        public Guid IdUser { get; set; }
        public User User { get; set; }
    }
}
