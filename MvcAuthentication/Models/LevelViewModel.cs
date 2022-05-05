using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;

namespace MvcAuthentication.Models
{
    public class LevelViewModel
    {
        public LevelProgressState LevelProgressState { get; set; }
        public Question CurrentQuestion { get; set; }
    }
}
