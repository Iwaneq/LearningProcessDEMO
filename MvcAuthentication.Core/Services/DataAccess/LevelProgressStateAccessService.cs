using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.Data;
using MvcAuthentication.Core.Services.DataAccess.Interfaces;
using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services
{
    public class LevelProgressStateAccessService : BaseDataAccessService, ILevelProgressStateAccessService
    {
        public LevelProgressStateAccessService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<LevelProgressState>> GetLevelProgressStatesWithLevels(int accountId)
        {
            return await _dataContext.LevelProgressStates.Where(x => x.Account.Id == accountId)
                .Include(l => l.Level)
                .ToListAsync();
        }

        public async Task<LevelProgressState> GetLevelProgressStateWithLevels(int levelStateId)
        {
            return await _dataContext.LevelProgressStates.Where(x => x.Id == levelStateId)
                .Include(l => l.Level)
                .Include(l => l.UnansweredQuestions)
                .Include(l => l.Level.LevelQuestions)
                    .ThenInclude(l => l.Question)
                        .ThenInclude(l => l.GoodAnswer)
                .Include(l => l.Level.LevelQuestions)
                    .ThenInclude(l => l.Question)
                            .ThenInclude(q => q.QuestionAnswers)
                                .ThenInclude(q => q.Answer)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateLevelState(LevelProgressState state)
        {
            _dataContext.LevelProgressStates.Update(state);
            await _dataContext.SaveChangesAsync();
        }
    }
}
