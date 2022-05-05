using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.Data;
using MvcAuthentication.Core.ManyToMany;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.Identity
{
    public class AccountService
    {
        private readonly DataContext _dataContext;

        public AccountService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Account> CreateAccount(Credential credentials)
        {
            //Create new Account Model
            Account newAccount = new Account()
            {
                Username = credentials.Username,
                LevelsProgress = new List<LevelProgressState>()
            };

            //Populate LevelProgressState List 
            await CreateLevelProgressStates(newAccount.LevelsProgress);

            //Hash Password
            var hasher = new PasswordHasher<Account>();
            var passwordHash = hasher.HashPassword(newAccount, credentials.Password);

            newAccount.PasswordHash = passwordHash;
            return newAccount;
        }

        private async Task CreateLevelProgressStates(List<LevelProgressState> levelProgressStates)
        {
            var levels = await _dataContext.Levels
                .Include(l => l.LevelQuestions)
                .ThenInclude(q => q.Question).ToListAsync();

            foreach(var level in levels)
            {
                //Create new LevelProgressState
                LevelProgressState newLevelState = new LevelProgressState()
                {
                    Level = level,
                    IsFinished = false,
                    ProgressPrecentage = 0
                };

                //Get all questions from level
                var levelQuestions = level.LevelQuestions.ToList();

                //Create list of Unanswered Questions
                List<UnansweredQuestion> unansweredQuestions = new List<UnansweredQuestion>();

                foreach(var question in levelQuestions)
                {
                    unansweredQuestions.Add(new UnansweredQuestion() { Question = question.Question, LevelProgressState = newLevelState });
                }

                //Assing UnasnweredQuestions list to new LevelProgressState
                newLevelState.UnansweredQuestions = unansweredQuestions;

                levelProgressStates.Add(newLevelState);
            }
        }
    }
}
