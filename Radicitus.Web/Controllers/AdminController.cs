using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Entities;
using Radicitus.SqlProviders;

namespace Radicitus.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRadSqlProvider _adminRepo;

        public AdminController(IRadSqlProvider adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public IActionResult NewsFeed()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateNewsFeed(NewsFeed newsFeed)
        {
            var newsFeedId = await _adminRepo.CreateNewsFeed(newsFeed);
            return Json(new { NewsFeedId = newsFeedId, Message = newsFeedId > 0 ? "Your news feed item has been created successfully!" : "Sorry, looks like your news feed was not created :(." });
        }
    }
}