using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Services;
using MovieTimeProject.Models;

namespace MovieTimeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieService _movieService;

        public HomeController(ILogger<HomeController> logger, MovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            ViewModelHomePage viewModelHomePage = new ViewModelHomePage(_movieService);
            return View(viewModelHomePage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ViewModelHomePage
    {
        public IEnumerable<string> topRatedList;
        public IEnumerable<string> latestList;
        public IEnumerable<string> interestList;

        private MovieService _movieService;

        public ViewModelHomePage(MovieService movieService)
        {
            _movieService = movieService;
            Initialize();
        }
        public void Initialize()
        {
            topRatedList = _movieService.getTopRatedListOfMovies();
            latestList = _movieService.getLatestListOfMovies();
            interestList = _movieService.getMayInterestListOfMovies();
        }
    }
}
