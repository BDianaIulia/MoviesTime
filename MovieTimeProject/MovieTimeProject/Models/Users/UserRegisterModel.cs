using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Users
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public string Error { get; set; }
    }
}
