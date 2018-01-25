using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Radicitus.SqlProviders;
using Radicitus.Web.Extensions;
using Radicitus.Web.Models;

namespace Radicitus.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRadSqlProvider _sql;

        public AccountController(IRadSqlProvider sql)
        {
            _sql = sql;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View("LogIn");
            var hashedBytes = CryptographyExtensions.HashPasswordSha512(model.Password);
            var isAuthenticated = await _sql.AuthenticateUser(model.Username, hashedBytes).ConfigureAwait(false);
            if(!isAuthenticated)
                return View("LogIn");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
            await HttpContext.SignInAsync(principal).ConfigureAwait(false);

            return Redirect("/Home/RaidEfforts");
        }
        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync().ConfigureAwait(false);
            return Redirect("/Home/Index");
        }
    }
}