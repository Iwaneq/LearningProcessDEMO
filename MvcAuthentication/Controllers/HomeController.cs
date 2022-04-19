using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Models;
using MvcAuthentication.State;
using System.Diagnostics;
using System.Security.Claims;

namespace MvcAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            //Get Id
            int id;
            bool isIdParsed = int.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, out id);

            if (isIdParsed)
            {
                var levels = await _dataContext.LevelProgressStates.Where(x => x.Account.Id == id).ToListAsync();

                return View(levels);
            }
            else
            {
                return Error();
            }
            

            //return View(AccountState.CurrentAccount.LevelsProgress);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}