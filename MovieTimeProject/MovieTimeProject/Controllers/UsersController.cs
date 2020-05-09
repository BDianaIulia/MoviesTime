using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.ApplicationLogicLibrary.Services;
using MovieTime.DataAccessLibrary;
using MovieTimeProject.Models.Users;
using Microsoft.AspNetCore.Http;

namespace MovieTimeProject.Controllers
{
    public class UsersController : Controller
    {
        private UserService _userService;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(UserService userService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Users
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName,
                           user.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", new RouteValueDictionary(
                                            new { controller = "Users", action = "Profile", succeedMessage = "Login suceeded!" }));
                }
                else
                {
                    if (_userService.NameExists(user.UserName))
                    {
                        return View("ErrorLogin", new User { ErrorLogin = "Wrong password" });
                    }

                    return View("ErrorLogin", new User { ErrorLogin = "Wrong username" });
                }
            }
            return View("ErrorLogin", new User { ErrorLogin = "Sorry, an error occurs. Retry." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Password,RePassword")] UserRegisterModel userRegistration)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ValidRegister(new User { UserName = userRegistration.UserName, Password = userRegistration.Password }, userRegistration.RePassword))
                {
                    var user = new IdentityUser { UserName = userRegistration.UserName };
                    var result = await _userManager.CreateAsync(user, userRegistration.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        _userService.SaveUser(new User { UserName = userRegistration.UserName, Password = userRegistration.Password });


                        await _userManager.AddToRoleAsync(user, "user");
                        

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("ErrorInregistration", new UserRegisterModel { Error = "Password must have at least 6 characters." });
                    }
                }
                else
                {
                    if (_userService.NameExists(userRegistration.UserName))
                    {
                        return View("ErrorInregistration", new UserRegisterModel { Error = "Username already exists. You have to choose another one." });
                    }

                    return View("ErrorInregistration", new UserRegisterModel { Error = "Please, re-entry same password." });

                }
            }
            return View("ErrorInregistration", new UserRegisterModel { Error = "Sorry, an error occurs. Retry." });
        }

        public bool ValidNameForRegister(string userName)
        {
            return _userService.ValidNameForRegister(userName);
        }

        public async Task<IActionResult> Profile(string succeedMessage)
        {
            var user = await _userManager.GetUserAsync(User);
            User userData = _userService.GetUserByName(user.UserName);
            int commentsNumber = _userService.GetNumberOfComments(userData);

            if (succeedMessage != null)
            {
                return View(new UserProfileModelView
                {
                    user = userData,
                    seenList = _userService.GetSeenListFor(userData),
                    wishList = _userService.GetWishListFor(userData),
                    commentsNumber = commentsNumber,
                    succeedMessage = succeedMessage
                });
            }
            else
            {
                return View(new UserProfileModelView
                {
                    user = userData,
                    seenList = _userService.GetSeenListFor(userData),
                    wishList = _userService.GetWishListFor(userData),
                    commentsNumber = commentsNumber
                });
            }
        }

        
        public IActionResult ChoosingAPhoto()
        {
            return PartialView("ChoosingAPhoto");
        }

        [HttpPost]
        public async Task<IActionResult> ChoosingADefaultPhotoAsync(string filePath)
        {
            await _userService.SavePhotoUser(filePath, User);
            return PartialView("ChoosingAPhoto");
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(ChosenPhotosViewModel chosenPhoto)
        {
            if (chosenPhoto.file != null)
            {
                await _userService.UploadPhotoChosenAsync(chosenPhoto.file);
                try
                {
                    await _userService.SavePhotoUser(chosenPhoto.file.FileName, User);
                }
                catch (Exception ex) { }
            }
            else
            {
                try
                {
                    await _userService.SavePhotoUser(chosenPhoto.photoPath, User);
                }
                catch (Exception ex) { }
            }

            return PartialView("ChoosingAPhoto");
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhotoPredefined(string filePath)
        {
            if (filePath != null)
            {
                try
                {
                    await _userService.SavePhotoUser(filePath, User);
                }
                catch (Exception ex) { }
            }
            else
            {
                try
                {
                    await _userService.SavePhotoUser(filePath, User);
                }
                catch (Exception ex) { }
            }

            return PartialView("ChoosingAPhoto");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
