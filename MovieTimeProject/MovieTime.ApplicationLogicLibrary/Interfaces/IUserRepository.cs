using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool NameExists(string userName);
        void SaveUser(User newUser);
    }
}
