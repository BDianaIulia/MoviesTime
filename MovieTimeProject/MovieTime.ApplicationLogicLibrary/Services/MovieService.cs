﻿using Microsoft.AspNetCore.Identity;
using MovieTime.ApplicationLogicLibrary.Helpers;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Services
{
    public class MovieService
    {
        private IMovieRepository _movieRepository;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        public MovieService(IMovieRepository movieRepository, IUserRepository userRepository, ICommentRepository commentRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _movieRepository = movieRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public IList<Comment> GetCommentsListFor(Guid movieId)
        {
            Movie movie;
            try
            {
                movie = _movieRepository.getElementBy(movieId);
            }
            catch (Exception ex) { return null; }

            return movie.Comments.ToList();

        }

        public IEnumerable<Movie> getTopRatedListOfMovies()
        {
            return _movieRepository.getTopRatedListOfMovies();
        }

        public IEnumerable<Movie> getLatestListOfMovies()
        {
            return _movieRepository.getLatestListOfMovies();
        }

        public Movie GetElementBy(Guid? id)
        {
            return _movieRepository.getElementBy(id);
        }

        public IEnumerable<Movie> getMayInterestListOfMovies()
        {
            return _movieRepository.getMayInterestListOfMovies();
        }

        public async System.Threading.Tasks.Task AddToStatusListAsync(Guid idMovie, string status, System.Security.Claims.ClaimsPrincipal user)
        {
            var userObj = await _userManager.GetUserAsync(user);
            var userName = userObj.UserName;
            var userModel = _userRepository.GetUserByName(userName);

            UserMovieActivity userMovieActivity = new UserMovieActivity { Status = status, IdMovie = idMovie, IdUser = userModel.Id };
            _movieRepository.AddUserMovieActivity(userMovieActivity);
        }

        public MovieScores GetMovieScoresFor(Movie movie)
        {
            return new MovieScores(movie);
        }

        public async System.Threading.Tasks.Task AddCommentAsync(Comment comment, Guid idMovie, System.Security.Claims.ClaimsPrincipal user)
        {
            var userObj = await _userManager.GetUserAsync(user);
            var userName = userObj.UserName;
            var userModel = _userRepository.GetUserByName(userName);

            comment.IdMovie = idMovie;
            comment.IdUser = userModel.Id;
            _commentRepository.Add(comment);
            _commentRepository.SaveChanges();


            MovieRating movieRating = _movieRepository.GetMovieRatingFor(idMovie);

            if( movieRating == null )
                movieRating = new MovieRating { IdMovie = idMovie };

            int score = _movieRepository.GetActualScoreForMovie(idMovie);
            int numberOfReviews = _movieRepository.GetNumberOfReviews(idMovie);
            int actualScore = BuildNewScoreForMovie(movieRating, comment.ReviewScore, score, numberOfReviews);
            
            _movieRepository.AddMovieReview(idMovie, actualScore);
            _movieRepository.SaveMovieRating(idMovie, movieRating);
        }

        public int BuildNewScoreForMovie(MovieRating movieRating, int reviewScore, int score, int numberOfReviews)
        {
            Type type = movieRating.GetType();
            PropertyInfo prop = type.GetProperty(rating[reviewScore]);
            int lastValue = (int)prop.GetValue(movieRating);

            prop.SetValue(movieRating, lastValue + 1, null);

            return (score * numberOfReviews + reviewScore) / (numberOfReviews + 1);
        }


        private Dictionary<int, string> rating = new Dictionary<int, string>
        {
            { 1, "NumberOf1ReviewStars" },
            { 2, "NumberOf2ReviewStars" },
            { 3, "NumberOf3ReviewStars" },
            { 4, "NumberOf4ReviewStars" },
            { 5, "NumberOf5ReviewStars" }
        };

    }
}
