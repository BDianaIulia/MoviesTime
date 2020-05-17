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
        private int _numberOfMoviePerPage = 24;
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

            var x = User.IsInRole("user");
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

        public IActionResult ListAfterGenre(string genre)
        {
            if (genre == null)
            {
                return NotFound();
            }

            var listOfMovies = _movieService.GetMoviesAfter(genre);
            if (listOfMovies == null)
            {
                return NotFound();
            }

            int noOfPages = Convert.ToInt32(Math.Ceiling((decimal)listOfMovies.Count() / _numberOfMoviePerPage));

            return View(new MovieGalleryViewModel { movies = listOfMovies.Take(_numberOfMoviePerPage).ToList(), numberOfPages = noOfPages, currentPage = "1", genre = genre });
        }

        public IActionResult ListAfterGenreOnPage(string genre, string currentPage)
        {
            if (genre == null)
            {
                return NotFound();
            }

            var listOfMovies = _movieService.GetMoviesAfter(genre);
            if (listOfMovies == null)
            {
                return NotFound();
            }

            int noOfPages = Convert.ToInt32(Math.Ceiling((decimal)listOfMovies.Count() / _numberOfMoviePerPage));
            int lowerBound = (Convert.ToInt32(currentPage) - 1) * _numberOfMoviePerPage;

            if (lowerBound + _numberOfMoviePerPage > listOfMovies.Count())
            {
                _numberOfMoviePerPage = listOfMovies.Count() - lowerBound;
            }

            return PartialView("_listAfterGenrePartialView", new MovieGalleryViewModel { movies = listOfMovies.GetRange(lowerBound, _numberOfMoviePerPage), numberOfPages = noOfPages, currentPage = currentPage, genre = genre });
        }

        public async Task CreateCommentAsync(string reviewScore, string comment, Guid idMovie)
        {
            await _movieService.AddCommentAsync(new Comment { CommentText = comment, ReviewScore = Int32.Parse(reviewScore) }, idMovie, User);
        }

        public async Task AddToWishListAsync(Guid idMovie)
        {
            await _movieService.AddToStatusListAsync(idMovie, "wish", User);
        }

        public async Task AddToSeenListAsync(Guid idMovie)
        {
            await _movieService.AddToStatusListAsync(idMovie, "seen", User);
        }

    }
}
