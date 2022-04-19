using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.ManyToMany;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using MvcAuthentication.State;
using MvcAuthentication.Views.Account;
using System.Security.Claims;

namespace MvcAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
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
            /* --- CREATE ACCOUNT ---  */

            //Create new Account Model
            Account newAccount = new Account()
            {
                Username = credentials.Username,
                LevelsProgress = new List<LevelProgressState>()
                {
                    new LevelProgressState()
                    {
                        IsFinished = false,
                        LevelName = "FirstLevel",
                        ProgressPrecentage = 0,
                        LevelQuestions = new List<LevelQuestion>()
                        {
                            new LevelQuestion() 
                            {
                                
                            }
                        }
                    }
                }
            };

            //Hash Password
            var hasher = new PasswordHasher<Account>();
            var passwordHash = hasher.HashPassword(newAccount, credentials.Password);

            newAccount.PasswordHash = passwordHash;

            //Save Account Model
            _dataContext.Accounts.Add(newAccount);
            await _dataContext.SaveChangesAsync();

            //Redirect to Index Page
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(Credential credentials)
        {
            if (!ModelState.IsValid) return View();

            //Search for username in Database
            var account = _dataContext.Accounts
                .Where(x => x.Username == credentials.Username)
                //.Include(a => a.LevelsProgress)
                .FirstOrDefault();

            if (account == null) return View();

            var hasher = new PasswordHasher<Account>();
            var result = hasher.VerifyHashedPassword(account, account.PasswordHash, credentials.Password);

            if (result == PasswordVerificationResult.Failed) return View();

            AccountState.CurrentAccount = account;

            //Create Security Context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
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
    }
}
