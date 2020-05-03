using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Helpers;
using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.ApplicationLogicLibrary.Services;
using MovieTime.DataAccessLibrary;
using MovieTimeProject.Models.Movies;

namespace MovieTimeProject.Controllers
{
    public class MoviesController : Controller
    {
        private MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie movie;
            try
            {
                movie = _movieService.GetElementBy(id);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }

            MovieScores movieScores = _movieService.GetMovieScoresFor(movie);
            return View(new DetailsMovieViewModel { Movie = movie, MovieScores = movieScores });
        }

    }
}
