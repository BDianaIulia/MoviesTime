using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Users
{
    public class UserProfileModelView
    {
        public User user { get; set; }
        public IList<Movie> wishList { get; set; }
        public IList<Movie> seenList { get; set; }
        public int commentsNumber { get; set; }
        public string succeedMessage { get; set; }
    }
}
