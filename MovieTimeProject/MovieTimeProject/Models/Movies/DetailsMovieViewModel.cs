using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Movies
{
    public class DetailsMovieViewModel
    {
        public Movie Movie { get; set; }
        public int NumberOfReviewsForMovie { get; set; }
    }
}
