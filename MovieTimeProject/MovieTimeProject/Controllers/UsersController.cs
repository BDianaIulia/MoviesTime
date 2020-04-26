using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.ApplicationLogicLibrary.Services;
using MovieTime.DataAccessLibrary;
using MovieTimeProject.Models.Users;

namespace MovieTimeProject.Controllers
{
    public class UsersController : Controller
    {
        private UserService _userService;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public UsersController(UserService userService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
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
                    return RedirectToAction("Index", "Home");  //Reirect to MyProfile when ready
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
                if (_userService.ValidRegister(new User { UserName = userRegistration.UserName, Password = userRegistration.Password} , userRegistration.RePassword))
                {
                    var user = new IdentityUser { UserName = userRegistration.UserName };
                    var result = await _userManager.CreateAsync(user, userRegistration.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        _userService.SaveUser(new User { UserName = userRegistration.UserName, Password = userRegistration.Password });
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

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
