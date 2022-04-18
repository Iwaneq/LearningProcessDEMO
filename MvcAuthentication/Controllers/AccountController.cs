using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Views.Account;
using System.Security.Claims;

namespace MvcAuthentication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credential credentials)
        {
            if (!ModelState.IsValid) return View();

            if(credentials.Username == "admin" && credentials.Password == "test")
            {
                //Create Security Context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Country, "Poland"),
                    new Claim("Role", "Admin")
                };

                //Create Identity
                var identity = new ClaimsIdentity(claims, "CookieAuth");

                //Create Claims Principal
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                //Redirect to Index Page
                return RedirectToAction("Index", "Home");
            }
            else if(credentials.Username == "user" && credentials.Password == "test")
            {
                //Create Security Context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "user"),
                    new Claim(ClaimTypes.Country, "Poland"),
                    new Claim("Role", "User")
                };

                //Create Identity
                var identity = new ClaimsIdentity(claims, "CookieAuth");

                //Create Claims Principal
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                //Redirect to Index Page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
