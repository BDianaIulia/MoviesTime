using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Models
{
    public class Comment
    {
        [Key]
        public int IdComment { get; set; }
        public int ReviewScore { get; set; }
        public string CommentText { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
