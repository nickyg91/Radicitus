using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Entities;
using Radicitus.SqlProviders;

namespace Radicitus.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IRadSqlProvider _adminRepo;

        public AdminController(IRadSqlProvider adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<IActionResult> EventManager()
        {
            var events = await _adminRepo.GetAllEvents();
            return View(events);
        }
        
        public IActionResult NewsFeed()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int eventId)
        {
            var eventItem = (await _adminRepo.GetAllEvents()).FirstOrDefault(x => x.EventId == eventId);
            return PartialView("CreateEvent", eventItem);
        }

        public IActionResult CreateEvent()
        {
            return PartialView();
        }

        public PartialViewResult EventList()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> CreateEvent(Event radEvent)
        {
            var createdEvent = await _adminRepo.CreateEvent(radEvent);
            return Json(createdEvent);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEvent(Event radEvent)
        {
            var deletedEvent = await _adminRepo.DeleteEvent(radEvent);
            return Json(deletedEvent);
        }

        [HttpPost]
        public async Task<JsonResult> EditEvent(Event radEvent)
        {
            var editedEvent = await _adminRepo.EditEvent(radEvent);
            return Json(editedEvent);
        }

        [HttpPost]
        public async Task<JsonResult> CreateNewsFeed(NewsFeed newsFeed)
        {
            var newsFeedId = await _adminRepo.CreateNewsFeed(newsFeed);
            return Json(new { NewsFeedId = newsFeedId, Message = newsFeedId > 0 ? "Your news feed item has been created successfully!" : "Sorry, looks like your news feed was not created :(." });
        }
    }
}