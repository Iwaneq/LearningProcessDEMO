using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace GermanLearningAPI.Tags
{
    public class JwtAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Check HttpContext
            if (context.HttpContext == null) throw new ArgumentNullException("httpContext");

            //Get Jwt Token and check if it's not null
            var token = context.HttpContext.Request.Cookies["X-Access-Token"];

            if (string.IsNullOrEmpty(token))
            {
                throw new SecurityException("Couldn't get Access Token.");
            }

            //Read token and get configuration
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            //Validate config and Token
            if(config == null)
            {
                throw new NullReferenceException("config");
            }
            if(!ValidateToken(securityToken, config))
            {
                throw new SecurityException("Access token is not valid. It ethier is invalid or expired.");
            }

            //Read Token and assign it's values to User's Identity
            var claims = ReadToken(context.HttpContext.Request.Cookies["X-Access-Token"]);

            context.HttpContext.User.AddIdentity(new ClaimsIdentity(claims));
        }

        private bool ValidateToken(JwtSecurityToken token, IConfiguration config)
        {
            //Check Issuer
            if (!string.Equals(token.Issuer, config["Jwt:Issuer"])) return false;

            //Check Audience
            if (!string.Equals(token.Audiences.FirstOrDefault(), config["Jwt:Audience"])) return false;

            //Check ValidTo info
            if(token.ValidTo < DateTime.UtcNow) return false;

            return true;
        }

        private List<Claim> ReadToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList();
        }
    }
}
