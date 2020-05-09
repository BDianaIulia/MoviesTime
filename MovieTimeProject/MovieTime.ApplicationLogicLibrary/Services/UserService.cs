
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace MovieTime.ApplicationLogicLibrary.Services
{
    public class UserService
    {
        IUserRepository _userRepository;
        IMovieRepository _movieRepository;
        private IHostingEnvironment _hostingEnvironment;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public UserService(IUserRepository userRepository, IMovieRepository movieRepository, IHostingEnvironment environment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _hostingEnvironment = environment;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public bool ValidRegister(User user, string rePassword)
        {
            if (user.Password != rePassword)
                return false;

            if (_userRepository.NameExists(user.UserName))
                return false;

            return true;
        }

        public bool NameExists(string userName)
        {
            if (_userRepository.NameExists(userName))
                return true;

            return false;
        }

        public bool ValidNameForRegister(string userName)
        {
            if (_userRepository.NameExists(userName))
                return false;

            return true;
        }

        public void SaveUser(User user)
        {
            User newUser = new User { UserName = user.UserName, Password = user.Password };
            _userRepository.SaveUser(newUser);
        }

        public User GetUserByName(string userName)
        {
            return _userRepository.GetUserByName(userName);
        }

        public IList<Movie> GetSeenListFor(User user)
        {
            IList<Movie> seenList = new List<Movie>();
            if (user.RelatedListMovies != null)
            {
                foreach (var movieUserActivity in user.RelatedListMovies)
                {
                    if (movieUserActivity.Status == "seen")
                    {
                        seenList.Add(_movieRepository.getElementBy(movieUserActivity.IdMovie));
                    }
                }
            }
            return seenList;
        }

        public IList<Movie> GetWishListFor(User user)
        {
            IList<Movie> wishList = new List<Movie>();
            if (user.RelatedListMovies != null)
            {
                foreach (var movieUserActivity in user.RelatedListMovies)
                {
                    if (movieUserActivity.Status == "wish")
                    {
                        wishList.Add(_movieRepository.getElementBy(movieUserActivity.IdMovie));
                    }
                }
            }
            return wishList;
        }

        public int GetNumberOfComments(User user)
        {
            if (user.Comments == null)
                return 0;

            return user.Comments.Count;
        }

        public async System.Threading.Tasks.Task UploadPhotoChosenAsync(IFormFile chosenPhoto)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "profilePhotos");

            if (chosenPhoto.Length > 0)
            {
                var filePath = Path.Combine(uploads, chosenPhoto.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await chosenPhoto.CopyToAsync(fileStream);
                }
            }
        }

        public async System.Threading.Tasks.Task SavePhotoUser(string photoPath, System.Security.Claims.ClaimsPrincipal user)
        {
            try
            {
                var userObj = await _userManager.GetUserAsync(user);
                var userName = userObj.UserName;
                _userRepository.SavePhotoPath(photoPath, userName);
                _userRepository.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

 
    }
}
