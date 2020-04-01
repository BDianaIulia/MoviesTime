using MovieTime.ApplicationLogicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class BaseRepository<T> : IRepository<T>
    {
        public void Add(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetElement(T element)
        {
            throw new NotImplementedException();
        }
    }
}
