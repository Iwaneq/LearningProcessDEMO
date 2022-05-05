using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.Identity
{
    public class LoginService
    {
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
    }
}
