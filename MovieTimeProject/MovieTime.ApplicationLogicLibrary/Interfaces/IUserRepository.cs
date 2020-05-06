using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool NameExists(string userName);
        void SaveUser(User newUser);
        User GetUserByName(string userName);
        void SavePhotoPath(string photoPath, string userName);
    }
}
