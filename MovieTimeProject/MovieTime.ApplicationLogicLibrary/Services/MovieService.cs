using Microsoft.AspNetCore.Identity;
using MovieTime.ApplicationLogicLibrary.Helpers;
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

        public async System.Threading.Tasks.Task AddToWishListAsync(Guid idMovie, string status, System.Security.Claims.ClaimsPrincipal user)
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
        }
    }
}
