using GermanLearningAPI.Tags;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcAuthentication.Core.Services.DataAccess.Interfaces;
using MvcAuthentication.Core.Services.Identity.Interfaces;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security;
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

            //Generate Refresh and Access Tokens
            var refreshToken = Guid.NewGuid().ToString();
            var accessToken = _loginService.GenerateToken(account);

            //Save Refresh Token to Account
            account.RefreshToken = refreshToken;
            await _accountAccessService.UpdateAccount(account);

            //Save Tokens to Cookies
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.UtcNow.AddDays(7) });
            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok();
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            //Get Refresh Token
            var refreshToken = Request.Cookies["X-Refresh-Token"];

            //Validate Refresh Token and Check if it's attached to any account
            if (refreshToken == null) return NotFound("Refresh Token");

            var account = await _accountAccessService.GetAccountByRefreshTokenAsync(refreshToken);

            if (account == null) return NotFound();

            //Generate new Access Token
            var accessToken = _loginService.GenerateToken(account);

            //Save Access Token
            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok();
        }

        [HttpGet("DeleteCookies")]
        public IActionResult DeleteCookies()
        {
            Response.Cookies.Delete("X-Access-Token");

            return Ok();
        }

        [HttpGet("AuthorizeTest")]
        [JwtAuthorize]
        public IActionResult AuthorizeTest()
        {
            var claims = HttpContext.User.Claims;

            var name = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return Ok($"You are authorized {name}!");
        }
    }
}
