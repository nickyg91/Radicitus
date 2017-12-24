using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.SqlProviders;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRadSqlProvider _radSql;

        public HomeController(IRadSqlProvider radSql)
        {
            _radSql = radSql;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RaidEfforts()
        {
            var model = new RaidEffortsModel
            {
                Grids = await _radSql.GetAllGridsAsync()
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
