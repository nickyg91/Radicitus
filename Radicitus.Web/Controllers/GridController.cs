using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost, Authorize]
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

        [HttpPost, Authorize]
        public async Task<IActionResult> AddMembers(List<RadMemberModel> members)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new {Message = "Invalid data submitted."});
            }

            var radNumbers = new List<RadGridNumber>();
            var gridId = members.First().GridId;
            var memberDictionary = members.ToDictionary(x => x.MemberName, x => x.GridNumbers);
            
            foreach (var member in memberDictionary)
            {
                var radGridNumbers = member.Value.Select(x => new RadGridNumber
                {
                    GridId = gridId,
                    GridNumber = x,
                    RadMemberName = member.Key
                });
                radNumbers.AddRange(radGridNumbers);
            }

            var insertedMembers = (await _radSql.InsertRadGridNumbersAsync(radNumbers)).ToList();
            return insertedMembers.Count == radNumbers.Count 
                ? Json(new {Message = "All members were inserted correctly!", Members = insertedMembers}) 
                : Json(new { Message = "Error: Some members were not inserted."});
        }

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
            var generatedNumbers = new HashSet<int>();
            for (var i = 0; i < totalNumbers; i++)
            {
                var randomNumber = rand.Next(1, 100);
                while (gridNumbersTaken != null && gridNumbersTaken.Contains(randomNumber)
                    || generatedNumbers.Contains(randomNumber))
                {
                    randomNumber = rand.Next(1, 100);
                }
                generatedNumbers.Add(randomNumber);
            }

            return Json(new { numbers = string.Join(",", generatedNumbers) });
        }

        [HttpGet, Authorize]
        public void ClearGridState(int id)
        {
            Session.Remove(id.ToString());
            Session.CommitAsync();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> DrawWinner(int id)
        {
            var winner = await _radSql.DrawWinner(id);
            return Json(new {Message = winner == null ? "Nobody won!" : $"{winner.RadMemberName} won the board with # {winner.GridNumber}!"});
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddMember(RadMemberModel member)
        {
            var memberDictionary = Session
                .GetObjectFromJson<Dictionary<string, RadMemberModel>>(member.GridId.ToString());
            var takenNumbers = (await _radSql.GetMemberNumbersForGridAsync(member.GridId)).Select(x => x.Key);

            var allTakenNumbers = memberDictionary?.SelectMany(x => x.Value.GridNumbers).ToList() ?? new List<int>();
                
            allTakenNumbers.AddRange(takenNumbers);

            if (allTakenNumbers.Count + member.GridNumbers.Count > 100)
            {
                return Json(new {Message = "The amount of numbers provided would put the total over the limit."});
            }
            if (allTakenNumbers.Count == 100)
            {
                return Json(new { Message = "There are no available numbers left!" });
            }

            var duplicateNumbers = member.GridNumbers.Where(number => allTakenNumbers.Contains(number)).ToList();

            if (duplicateNumbers.Any())
            {
                Response.StatusCode = (int) HttpStatusCode.Found;
                return Json(
                    new
                    {
                        Message =
                        $"{member.MemberName} has not been added! Duplicate numbers found! {string.Join(",", duplicateNumbers)}"
                    });
            }

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
                    { member.MemberName.ToLower(), member }
                };
            }
            Session.SetObjectAsJson(member.GridId.ToString(), memberDictionary);
            return Json(
                new
                {
                    Message =
                    $"{member.MemberName} has been added! If your session expires, you will lose this information! Make sure to save before exiting."
                });
        }

        [HttpPost, Authorize]
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