using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.Services.DataAccess.Interfaces;
using MvcAuthentication.Core.Services.Identity.Interfaces;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;

namespace GermanLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAccessService _accountAccessService;
        private readonly ILoginService _loginService;
        private readonly IAccountService _accountService;

        #region Constructor / Setup

        public AccountController(IAccountAccessService accountAccessService,
            ILoginService loginService,
            IAccountService accountService)
        {
            _accountAccessService = accountAccessService;
            _loginService = loginService;
            _accountService = accountService;
        }

        #endregion

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(Credential credentials)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            //Create Account
            var newAccount = await _accountService.CreateAccount(credentials);

            //Save Account Model
            await _accountAccessService.SaveAccount(newAccount);

            //Redirect to Index Page
            return CreatedAtAction("RegisterUser", newAccount);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(Credential credentials)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            try
            {
                AccountState.CurrentAccount = await _accountAccessService.GetAccountAsync(credentials);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Create Security Context, Claims Principal and Sign In
            var claimsPrincipal = _loginService.CreateClaimsPrincipal();
            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            return Ok();
        }

        //[HttpGet]
        //public Account Test()
        //{
        //    return AccountState.CurrentAccount;
        //}

        [HttpGet("AuthorizeTest")]
        [Authorize]
        public IActionResult AuthorizeTest()
        {
            return Ok("You are authorized!");
        }
    }
}
