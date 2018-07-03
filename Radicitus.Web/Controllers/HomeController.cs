using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.SqlProviders;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRadSqlProvider _radRepo;
        public HomeController(IRadSqlProvider radRepo)
        {
            _radRepo = radRepo;
        }

        public async Task<IActionResult> Index()
        {
            var newsFeedItems = await _radRepo.GetLastTenFeeds();
            return View(newsFeedItems);
        }

        public IActionResult RaidEfforts()
        {
            return View();
        }

        public IActionResult CreateGridModal()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
