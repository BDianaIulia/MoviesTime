using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Services
{
    public class MovieService
    {
        private IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<Movie> getTopRatedListOfMovies()
        {
            return _movieRepository.getTopRatedListOfMovies();
        }

        public IEnumerable<Movie> getLatestListOfMovies()
        {
            return _movieRepository.getLatestListOfMovies();
        }

        public Movie getElementBy(Guid? id)
        {
            return _movieRepository.getElementBy(id);
        }

        public IEnumerable<Movie> getMayInterestListOfMovies()
        {
            return _movieRepository.getMayInterestListOfMovies();
        }
        public int getNumberOfReviews(Movie movie)
        {
            var movieRating = movie.MovieRating;

            if (movieRating != null)
            {
                return movieRating.NumberOf1ReviewStars + movieRating.NumberOf2ReviewStars +
                        movieRating.NumberOf3ReviewStars + movieRating.NumberOf4ReviewStars +
                        movieRating.NumberOf5ReviewStars;
            }
            else
            {
                return 0;
            }
        }
    }
}
