using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcAuthentication.Core.Services.DataAccess.Interfaces;
using MvcAuthentication.Core.Services.Identity.Interfaces;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using System.Security.Claims;
using System.Text;

namespace GermanLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAccessService _accountAccessService;
        private readonly ILoginService _loginService;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        #region Constructor / Setup

        public AccountController(IAccountAccessService accountAccessService,
            ILoginService loginService,
            IAccountService accountService, IConfiguration config)
        {
            _accountAccessService = accountAccessService;
            _loginService = loginService;
            _accountService = accountService;
            _config = config;
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
        public async Task<IActionResult> LoginUser([FromBody] Credential credentials)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            var account = await _accountAccessService.GetAccountAsync(credentials);

            if (account == null) return NotFound();

            //Create Security Context, Claims Principal and Sign In
            var refreshToken = new Guid();
            var accessToken = GenerateToken(account);

            Response.Cookies.Append("X-Refresh-Token", refreshToken.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });


            return Ok();
        }

        private string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(type: "ID", account.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Username)
            };


        }

        [HttpGet("AuthorizeTest")]
        [Authorize]
        public IActionResult AuthorizeTest()
        {
            return Ok("You are authorized!");
        }
    }
}
