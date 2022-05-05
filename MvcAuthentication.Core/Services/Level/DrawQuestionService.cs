using MvcAuthentication.Core.Data;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.Level
{
    public class DrawQuestionService
    {
        private readonly DataContext _dataContext;

        public DrawQuestionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Question DrawQuestion(LevelProgressState levelProgressState)
        {
            var random = new Random();

            return levelProgressState.UnansweredQuestions[random.Next(0, levelProgressState.UnansweredQuestions.Count)].Question;
        }
    }
}
