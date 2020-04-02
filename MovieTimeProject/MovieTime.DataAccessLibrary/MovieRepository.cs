using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        MovieContext _db;
        public MovieRepository(MovieContext db) : base(db)
        {
            _db = db;
        }

    }
}
