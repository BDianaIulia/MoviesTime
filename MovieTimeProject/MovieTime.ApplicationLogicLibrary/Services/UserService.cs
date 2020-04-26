using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Services
{
    public class UserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
