using MovieTime.ApplicationLogicLibrary.Interfaces;
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

        public IEnumerable<string> getTopRatedListOfMovies()
        {
            return _movieRepository.getTopRatedListOfMovies();
        }

        public IEnumerable<string> getLatestListOfMovies()
        {
            return _movieRepository.getLatestListOfMovies();
        }

        public IEnumerable<string> getMayInterestListOfMovies()
        {
            return _movieRepository.getMayInterestListOfMovies();
        }
    }
}
