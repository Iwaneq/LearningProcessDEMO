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
    public class QuestionService
    {
        private readonly DataContext _dataContext;

        public QuestionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Question DrawQuestion(LevelProgressState levelProgressState)
        {
            if(levelProgressState.UnansweredQuestions.Count == 0)
            {
                levelProgressState.IsFinished = true;
                return null;
            }
            var random = new Random();

            return levelProgressState.UnansweredQuestions[random.Next(0, levelProgressState.UnansweredQuestions.Count)].Question;
        }

        public void RemoveQuestion(LevelProgressState levelProgressState, Question answeredQuestion)
        {
            //Get UnansweredQuestion object
            var unansweredQuestion = levelProgressState.UnansweredQuestions.Where(x => x.QuestionId == answeredQuestion.Id).FirstOrDefault();

            //Remove UnansweredQuestion object from DB and LevelProgressState
            _dataContext.UnansweredQuestion.Remove(unansweredQuestion);
            levelProgressState.UnansweredQuestions.RemoveAll(x => x.QuestionId == answeredQuestion.Id);
        }
    }
}
