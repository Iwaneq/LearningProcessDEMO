using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.ManyToMany;
using MvcAuthentication.Core.Services;
using MvcAuthentication.Core.Services.Identity;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using MvcAuthentication.Views.Account;
using System.Security.Claims;

namespace MvcAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountAccessService _accountAccessService;
        private readonly LoginService _loginService;
        private readonly AccountService _accountService;

        public AccountController(DataContext dataContext, 
            AccountAccessService accountAccessService, 
            LoginService loginService, 
            AccountService accountService)
        {
            _accountAccessService = accountAccessService;
            _loginService = loginService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(Credential credentials)
        {
            if (!ModelState.IsValid) return View();

            /* --- CREATE ACCOUNT ---  */
            var newAccount = await _accountService.CreateAccount(credentials);

            //Save Account Model
            await _accountAccessService.SaveAccount(newAccount);

            //Redirect to Index Page
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(Credential credentials)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                AccountState.CurrentAccount = await _accountAccessService.GetAccountAsync(credentials);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //Create Security Context, Claims Principal and Sign In
            var claimsPrincipal = _loginService.CreateClaimsPrincipal();
            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            //Redirect to Index Page
            return RedirectToAction("Index", "Home");
        }
    }
}
