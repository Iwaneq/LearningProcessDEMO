using GermanLearningAPI.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.Services.DataAccess.Interfaces;
using System.Security.Claims;

namespace GermanLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelProgressStatesController : ControllerBase
    {
        private readonly ILevelProgressStateAccessService _levelProgressStateAccess;

        #region Constructor / Setup

        public LevelProgressStatesController(ILevelProgressStateAccessService levelProgressStateAccess)
        {
            _levelProgressStateAccess = levelProgressStateAccess;
        } 

        #endregion

        [HttpGet("GetLevelProgressStates")]
        [JwtAuthorize]
        public async Task<IActionResult> GetLevelProgressStates()
        {
            var claims = HttpContext.User.Claims;

            int id;
            bool isIdParsed = int.TryParse(claims.FirstOrDefault(x => x.Type == "ID").Value, out id);

            if(!isIdParsed)
            {
                return NotFound("Cannot found user with that ID");
            }

            var levels = await _levelProgressStateAccess.GetLevelProgressStatesWithLevels(id);

            return Ok(levels);
        }
    }
}
