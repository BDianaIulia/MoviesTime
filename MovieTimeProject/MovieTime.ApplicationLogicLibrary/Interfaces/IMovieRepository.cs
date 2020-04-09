using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> getTopRatedListOfMovies();
        IEnumerable<Movie> getLatestListOfMovies();
        IEnumerable<Movie> getMayInterestListOfMovies();
    }
}
