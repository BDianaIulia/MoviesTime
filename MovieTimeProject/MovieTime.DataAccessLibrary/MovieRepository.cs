using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTime.ApplicationLogicLibrary.Exceptions;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

        public void AddMovieReview(Guid idMovie, int reviewScore)
        {
            var movieObj = (from movie in _db.Movie
                    where movie.Id == idMovie
                    select movie).SingleOrDefault();

            movieObj.ReviewScoreValue = reviewScore;
            _db.Update(movieObj);
            _db.SaveChanges();
        }

        public void AddUserMovieActivity(UserMovieActivity userMovieActivity)
        {
            _db.Add(userMovieActivity);
            _db.SaveChanges();
        }

        public int GetActualScoreForMovie(Guid idMovie)
        {
            return (from movie in _db.Movie
                    where movie.Id == idMovie
                    select movie.ReviewScoreValue).SingleOrDefault();
        }

        public List<Movie> GetAllWithGenre(string genre)
        {
            return (from movie in _db.Movie.Include(x => x.Genres).ThenInclude(x => x.Genre)
                    where movie.Genres.FirstOrDefault(x => x.Genre.GenreName == genre) != null
                    select movie).ToList();
        }

        public Movie getElementBy(Guid? id)
        {
            if (id == null)
            {
                throw new Exception("Empty id");
            }

            var searchedMovie = (from movie in _db.Movie.Include(x => x.Genres).ThenInclude(x => x.Genre)
                                                        .Include(x => x.MovieRating)
                                                        .Include(x => x.Comments).ThenInclude(x => x.User)
                                 where movie.Id == id
                                 select movie).SingleOrDefault();

            if (searchedMovie == null)
            {
                throw new EntityNotFoundException(id);
            }

            return searchedMovie;
        }

        public IEnumerable<Movie> getLatestListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.ReleaseDate descending
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public IEnumerable<Movie> getMayInterestListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.Id
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public MovieRating GetMovieRatingFor(Guid idMovie)
        {
            return (from movie in _db.Movie.Include(x => x.MovieRating)
                    where movie.Id == idMovie
                    select movie.MovieRating).SingleOrDefault();
        }

        public int GetNumberOfReviews(Guid idMovie)
        {
            var movieRating =  (from movie in _db.Movie.Include(x => x.MovieRating)
                            where movie.Id == idMovie
                            select movie.MovieRating).SingleOrDefault();

            if (movieRating != null)
            {
                return movieRating.NumberOf1ReviewStars + movieRating.NumberOf2ReviewStars +
                        movieRating.NumberOf3ReviewStars + movieRating.NumberOf4ReviewStars
                        + movieRating.NumberOf5ReviewStars;
            }
            return 0;
        }

        public IEnumerable<Movie> getTopRatedListOfMovies()
        {
            return (from movie in _db.Movie
                    orderby movie.Popularity descending
                    select movie).Take(_configuration.GetValue<int>("NumberOfMoviesToBeDisplayed"));
        }

        public void SaveMovieRating(Guid idMovie, MovieRating movieRating)
        {
            _db.Update(movieRating);
            _db.SaveChanges();
        }
    }
}
