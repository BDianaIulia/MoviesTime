using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<string> getTopRatedListOfMovies();
        IEnumerable<string> getLatestListOfMovies();
        IEnumerable<string> getMayInterestListOfMovies();
    }
}
