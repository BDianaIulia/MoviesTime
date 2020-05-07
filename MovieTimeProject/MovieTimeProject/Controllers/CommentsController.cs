using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Models;
using MovieTime.DataAccessLibrary;

namespace MovieTimeProject.Controllers
{
    public class CommentsController : Controller
    {
        private readonly MovieContext _context;

        public CommentsController(MovieContext context)
        {
            _context = context;
        }

 
        public IActionResult Create()
        {
            return View();
        }
    }
}
