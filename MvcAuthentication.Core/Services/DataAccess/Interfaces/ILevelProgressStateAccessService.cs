using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.DataAccess.Interfaces
{
    public interface ILevelProgressStateAccessService
    {
        Task<List<LevelProgressState>> GetLevelProgressStatesWithLevels(int accountId);
    }
}
