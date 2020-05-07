using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MovieContext db) : base(db)
        {
        }


    }
}
