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

        public IEnumerable<string> getLatestListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.ReleaseDate descending
                    select movie.PosterPath).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public IEnumerable<string> getMayInterestListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.Id
                    select movie.PosterPath).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public IEnumerable<string> getTopRatedListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.Popularity descending
                    select movie.PosterPath).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }
    }
}
