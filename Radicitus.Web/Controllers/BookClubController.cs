using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class BookClubController : Controller
    {
        public IActionResult Index()
        {
            List<BookModel> model = new List<BookModel> { new BookModel { Title = "Annihilation", Description = "Pretty weird", ImageName = "Annihilation.jpg" }, new BookModel { Title = "The Long Way to a Small Angry Planet", Description = "A very fun book we all enjoyed. Elk and Kelborn said they'd fuck a lizard", ImageName = "longway.jpg" }, new BookModel { Title = "Dune", Description = "I already read it a month before", ImageName = "dune.jpg" } };
            return View(model);
        }

    }
}