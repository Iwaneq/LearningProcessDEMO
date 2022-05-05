using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.Services;
using MvcAuthentication.Core.Services.Level;
using MvcAuthentication.Core.State;
using MvcAuthentication.Models;

namespace MvcAuthentication.Controllers
{
    public class LevelController : Controller
    {
        private LevelProgressStateAccessService _levelProgressStateAccess;
        private readonly DrawQuestionService _questionService;
        public LevelProgressState LevelProgressState { get; set; }
        public Question CurrentQuestion { get; set; }

        public LevelController(LevelProgressStateAccessService levelProgressStateAccess, DrawQuestionService questionService)
        {
            _levelProgressStateAccess = levelProgressStateAccess;
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLevel(int id)
        {
            LevelViewModel levelViewModel = new LevelViewModel();

            levelViewModel.LevelProgressState = await _levelProgressStateAccess.GetLevelProgressStateWithLevels(id);

            if (LevelProgressState == null) return NotFound();

            levelViewModel.CurrentQuestion = _questionService.DrawQuestion(LevelProgressState);

            return View("/Index", levelViewModel);
        }



        //[HttpGet]
        //public IActionResult GetQuestion()
        //{

        //}
    }
}
