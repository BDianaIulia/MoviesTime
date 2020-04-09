using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        IConfiguration _configuration;
        public MovieRepository(IConfiguration configuration, MovieContext db) : base(db)
        {
            _configuration = configuration;
        }

        public IEnumerable<Movie> getLatestListOfMovies()
        {
            return (from movie in _db.Movie.Include(x => x.Genres).ThenInclude(x => x.Genre)
                    orderby movie.ReleaseDate descending
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public IEnumerable<Movie> getMayInterestListOfMovies()
        {
            return (from movie in _db.Movie.Include(x => x.Genres).ThenInclude(x => x.Genre)
                    orderby movie.Id
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public IEnumerable<Movie> getTopRatedListOfMovies()
        {
            return (from movie in _db.Movie.Include(x => x.Genres).ThenInclude(x => x.Genre)
                    orderby movie.Popularity descending
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }
    }
}
