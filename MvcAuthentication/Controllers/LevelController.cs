using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.Helpers;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.Services;
using MvcAuthentication.Core.Services.Level;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.ViewModels;
using MvcAuthentication.Models;

namespace MvcAuthentication.Controllers
{
    public class LevelController : Controller
    {
        [BindProperty]
        public static LevelViewModel LevelViewModel { get; set; }

        private LevelProgressStateAccessService _levelProgressStateAccess;
        private readonly QuestionService _questionService;
        private readonly DataContext _dataContext;

        public LevelProgressState LevelProgressState { get; set; }
        public Question CurrentQuestion { get; set; }

        public LevelController(LevelProgressStateAccessService levelProgressStateAccess, 
            QuestionService questionService, 
            DataContext dataContext)
        {
            _levelProgressStateAccess = levelProgressStateAccess;
            _questionService = questionService;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetLevel(int id)
        {
            //Instantinate new LevelViewModel for currently playing level
            LevelViewModel = new LevelViewModel();

            //Get LevelProgressState from DB
            LevelViewModel.LevelProgressState = await _levelProgressStateAccess.GetLevelProgressStateWithLevels(id);

            //Draw CurrentQuestion
            LevelViewModel.CurrentQuestion = _questionService.DrawQuestion(LevelViewModel.LevelProgressState);

            //Save in TempData GoodAnswerId, so User don't have access to it from url
            TempData["GoodAnswerId"] = LevelViewModel.CurrentQuestion.GoodAnswer.Id;

            return View("Index", LevelViewModel);
        }

        public async Task<IActionResult> VerifyAnswer(int answerId)
        {
            //Get GoodAnswerId
            var goodAnswerId = TempData["GoodAnswerId"];
            if (goodAnswerId == null)
            {
                return NotFound();
            }

            //If answer is good, delete current from UnansweredQuestions in LevelProgressState, draw new Question and Save Changes to the DB
            else if (goodAnswerId.ToString() == answerId.ToString())
            {
                //Remove answered question
                _questionService.RemoveQuestion(LevelViewModel.LevelProgressState, LevelViewModel.CurrentQuestion);

                //Draw new question
                LevelViewModel.CurrentQuestion = _questionService.DrawQuestion(LevelViewModel.LevelProgressState);

                //Calculate ProgressPrecentage
                LevelViewModel.LevelProgressState.ProgressPrecentage = ProgressPrecentageCalculator.Calculate(LevelViewModel.LevelProgressState.UnansweredQuestions,
                    LevelViewModel.LevelProgressState.Level.LevelQuestions);

                if (LevelViewModel.CurrentQuestion == null)
                {
                    //Update and save LevelProgressState
                    await _levelProgressStateAccess.UpdateLevelState(LevelViewModel.LevelProgressState);

                    return RedirectToAction("Index", "Home");
                }

                TempData["GoodAnswerId"] = LevelViewModel.CurrentQuestion.GoodAnswer.Id;

                //Update and save LevelProgressState
                await _levelProgressStateAccess.UpdateLevelState(LevelViewModel.LevelProgressState);
            }
            else
            {
                //If Anwser is bad, re-save GoodAnswerId in TempData 
                TempData["GoodAnswerId"] = LevelViewModel.CurrentQuestion.GoodAnswer.Id;
            }

            return View("Index", LevelViewModel);
        }
    }
}
