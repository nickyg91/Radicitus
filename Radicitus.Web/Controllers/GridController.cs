using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Entities;
using Radicitus.SqlProviders;
using Radicitus.Web.Extensions;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class GridController : Controller
    {
        private readonly IRadSqlProvider _radSql;
        private ISession Session => HttpContext.Session;
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
            var isFreshGrid = gridNumbersTaken == null;
            var unsavedGridNumbers = Session.GetObjectFromJson<Dictionary<string, RadMemberModel>>(gridId.ToString());
            if (unsavedGridNumbers != null)
            {
                gridNumbersTaken = isFreshGrid ? new HashSet<int>() : gridNumbersTaken; 
                var takenNumbersToAdd = unsavedGridNumbers.SelectMany(x => x.Value.GridNumbers).ToHashSet();
                foreach (var number in takenNumbersToAdd)
                {
                    gridNumbersTaken.Add(number);
                }
            }
            var rand = new Random();
            var generatedNumbers = new List<int>();
            for (var i = 0; i < totalNumbers; i++)
            {
                var randomNumber = rand.Next(1, 100);
                while (gridNumbersTaken != null && gridNumbersTaken.Contains(randomNumber))
                {
                    randomNumber = rand.Next(1, 100);
                }
                generatedNumbers.Add(randomNumber);
            }

            return Json(new { numbers = string.Join(",", generatedNumbers) });
        }

        [HttpPost]
        public IActionResult AddMember(RadMemberModel member)
        {
            var memberDictionary = Session
                .GetObjectFromJson<Dictionary<string, RadMemberModel>>(member.GridId.ToString());
            if (memberDictionary != null)
            {
                var isMemberStored = memberDictionary.ContainsKey(member.MemberName.ToLower());
                if (isMemberStored)
                {
                    Response.StatusCode = (int) HttpStatusCode.Found;
                    return Json(
                        new
                        {
                            Message =
                            $"{member.MemberName} has been added to this grid already. To change their numbers, remove and re-add them."
                        });
                }
                memberDictionary.Add(member.MemberName, member);
                Session.SetObjectAsJson(member.GridId.ToString(), memberDictionary);
            }
            else
            {
                memberDictionary = new Dictionary<string, RadMemberModel>
                {
                    { member.MemberName, member }
                };
                Session.SetObjectAsJson(member.GridId.ToString(), memberDictionary);
            }
            return Json(
                new
                {
                    Message =
                    $"{member.MemberName} has been added! If your session expires, you will lose this information! Make sure to save before exiting."
                });
        }

        [HttpPost]
        public IActionResult RemoveMember(RadMemberModel member)
        {
            var memberDictionary = Session
                .GetObjectFromJson<Dictionary<string, RadMemberModel>>(member.GridId.ToString());
            if (memberDictionary.ContainsKey(member.MemberName))
            {
                memberDictionary.Remove(member.MemberName);
                return Json(
                    new
                    {
                        Message =
                        $"{member.MemberName} has been removed."
                    });
            }
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return Json(
                new
                {
                    Message =
                    $"{member.MemberName} does not exist inside this grid! Add them!"
                });
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