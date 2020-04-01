using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetElement(T element);
        void Add(T element);
    }
}
