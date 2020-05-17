using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Movies
{
    public class MovieGalleryViewModel
    {
        public List<Movie> movies { get; set; }
        public int numberOfPages { get; set; }
        public string currentPage { get; set; }
        public string searched { get; set; }
    }
}
