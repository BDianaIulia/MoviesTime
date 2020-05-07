using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Linq;

namespace MovieTime.DataAccessLibrary
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MovieContext db) : base(db)
        {
        }

        public User GetUserByName(string userName)
        {
            return (from user in _db.User.Include(x => x.Comments)
                                         .Include(x => x.RelatedListMovies)
                    where user.UserName == userName
                   select user).FirstOrDefault();
        }

        public bool NameExists(string userName)
        {
            if ((from user in _db.User
                 where user.UserName == userName
                 select user).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        public void SavePhotoPath(string photoPath, string userName)
        {
            var userModel = (from user in _db.User
                        where user.UserName == userName
                        select user).FirstOrDefault();

            if (userModel == null)
                throw new Exception();

            userModel.PhotoPath = photoPath;
            _db.Update(userModel);
        }

        public void SaveUser(User newUser)
        {
            _db.Add(newUser);
            SaveChanges();
        }
    }
}
