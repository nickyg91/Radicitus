using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Entities;

namespace Radicitus.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult NewsFeed()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CreateNewsFeed(NewsFeed newsFeed)
        {
            return Json(null);
        }
    }
}