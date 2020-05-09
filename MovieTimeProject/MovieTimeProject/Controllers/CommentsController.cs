using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.ApplicationLogicLibrary.Services;
using MovieTime.DataAccessLibrary;

namespace MovieTimeProject.Controllers
{
    public class CommentsController : Controller
    {
        private MovieService _movieService;

        public CommentsController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index(Guid movieId)
        {
            if (movieId != null)
            {
                IList<Comment> commentsList = _movieService.GetCommentsListFor(movieId);

                return View(commentsList);
            }

            return BadRequest();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
