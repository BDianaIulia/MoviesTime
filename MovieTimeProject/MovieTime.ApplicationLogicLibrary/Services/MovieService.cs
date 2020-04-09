﻿using MovieTime.ApplicationLogicLibrary.Interfaces;
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

        public IEnumerable<Movie> getMayInterestListOfMovies()
        {
            return _movieRepository.getMayInterestListOfMovies();
        }
    }
}
