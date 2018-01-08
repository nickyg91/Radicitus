using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Entities;
using Radicitus.SqlProviders;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class GridController : Controller
    {
        private readonly IRadSqlProvider _radSql;
        
        public GridController(IRadSqlProvider radSql)
        {
            _radSql = radSql;
        }

        [HttpPost]
        public async Task<IActionResult> AddGrid(GridModel grid)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ModelState);
            }

            var insertedGrid = await _radSql.InsertGridAsync(new Grid
            {
                CostPerSquare = grid.CostPerSquare,
                GridName = grid.GridName
            }).ConfigureAwait(false);
            return Json(insertedGrid);
        }

        [HttpGet]
        public IActionResult GetAllGridsPartial()
        {
            return ViewComponent("RenderAllGrids");
        }

        public async Task<IActionResult> ViewGrid(int id)
        {
            return View(new ViewGridModel
            {
                Grid = await _radSql.GetGridByGridIdAsync(id),
                MemberNumbers = await _radSql.GetMemberNumbersForGridAsync(id),
                UsedNumbers = await _radSql.GetAllUsedNumbersForGridAsync(id)
            });
        }
    }
}