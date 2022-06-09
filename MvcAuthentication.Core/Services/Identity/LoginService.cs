using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MvcAuthentication.Core.Services.Identity.Interfaces;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.Identity
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;

        #region Constructor / Setup

        public LoginService(IConfiguration config)
        {
            _config = config;
        } 

        #endregion

        public ClaimsPrincipal CreateClaimsPrincipal()
        {
            //Create Security Context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, AccountState.CurrentAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, AccountState.CurrentAccount.Username),
                new Claim(ClaimTypes.Country, "Poland"),
                new Claim("Role", "Admin")
            };

            //Create Identity
            var identity = new ClaimsIdentity(claims, "CookieAuth");

            //Return Claims Principal
            return new ClaimsPrincipal(identity);
        }

        public string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(type: "ID", account.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Username)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
