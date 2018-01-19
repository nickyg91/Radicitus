using System;
using System.Collections.Generic;
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

        //[HttpPost]
        //public async Task<IActionResult> AddMembers(AddMembersModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int) HttpStatusCode.BadRequest;
        //        return Json(new {Message = "Invalid data submitted."});
        //    }

        //    var radNumbers = new List<RadGridNumber>();

        //    foreach (var member in model.Members)
        //    {
                
        //    }

        //    var insertedMembers = await _radSql.InsertRadGridNumbersAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> RandomizeNumbers(int totalNumbers, int gridId)
        {
            var gridNumbersTaken = await _radSql.GetAllUsedNumbersForGridAsync(gridId);
            var rand = new Random();
            var generatedNumbers = new List<int>();
            for (var i = 0; i < totalNumbers; i++)
            {
                var randomNumber = rand.Next(1, 100);
                while (gridNumbersTaken != null && !gridNumbersTaken.Contains(randomNumber))
                {
                    randomNumber = rand.Next(1, 100);
                }
                generatedNumbers.Add(randomNumber);
            }

            return Json(new { numbers = string.Join(",", generatedNumbers) });
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