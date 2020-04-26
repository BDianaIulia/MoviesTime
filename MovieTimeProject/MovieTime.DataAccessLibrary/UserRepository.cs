using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;

using System.Linq;

namespace MovieTime.DataAccessLibrary
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MovieContext db) : base(db)
        {
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

        public void SaveUser(User newUser)
        {
            _db.Add(newUser);
            SaveChanges();
        }
    }
}
