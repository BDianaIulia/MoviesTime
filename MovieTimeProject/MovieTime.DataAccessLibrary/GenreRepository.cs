﻿using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using System.Linq;

namespace MovieTime.DataAccessLibrary
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieContext db) : base(db)
        {
        }

        public Genre GetGenreAfterName(string genreName)
        {
            var genreSearchedFor = (from genre in _db.Genre
                                    where genre.GenreName == genreName
                                    select genre).ToList().FirstOrDefault();

            return genreSearchedFor;
        }
    }
}
