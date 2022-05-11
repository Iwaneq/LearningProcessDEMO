using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;

namespace MvcAuthentication.Core.ViewModels
{
    public class LevelViewModel
    {
        [BindProperty]
        public LevelProgressState LevelProgressState { get; set; }

        [BindProperty]
        public Question CurrentQuestion { get; set; }
    }
}
