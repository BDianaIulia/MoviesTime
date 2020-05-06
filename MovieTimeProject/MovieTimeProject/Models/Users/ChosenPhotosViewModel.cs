using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Users
{
    public class ChosenPhotosViewModel
    {
        public IFormFile file { get; set; }
        public string photoPath { get; set; }
    }
}
