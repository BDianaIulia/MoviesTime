using MovieTime.ApplicationLogicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class BaseRepository<T> : IRepository<T>
    {
        MovieContext _db;
        public BaseRepository(MovieContext db)
        {
            _db = db;
        }
        public void Add(T element)
        {
            _db.Add(element);
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetElement(T element)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
