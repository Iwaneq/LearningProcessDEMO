using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.Services;
using MvcAuthentication.Core.Services.Level;
using MvcAuthentication.Core.State;

namespace MvcAuthentication.Views.Level
{
    public class IndexModel : PageModel
    {
        //private readonly DrawQuestionService _questionService;
        //private readonly LevelProgressStateAccessService _levelProgressStateAccess;

        //public IndexModel(DrawQuestionService questionService,
        //    LevelProgressStateAccessService levelProgressStateAccess)
        //{
        //    _questionService = questionService;
        //    _levelProgressStateAccess = levelProgressStateAccess;
        //}

        //public LevelProgressState LevelProgressState { get; set; }

        //public Question CurrentQuestion { get; set; }

        public async Task OnGet(int id)
        {
            //LevelProgressState = await _levelProgressStateAccess.GetLevelProgressStateWithLevels(id);

            //if (LevelProgressState == null) throw new KeyNotFoundException();

            //CurrentQuestion = _questionService.DrawQuestion(LevelProgressState);
        }
    }
}
