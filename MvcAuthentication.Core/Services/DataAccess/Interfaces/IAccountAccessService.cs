using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.DataAccess.Interfaces
{
    public interface IAccountAccessService
    {
        Task<Account> GetAccountAsync(Credential credentials);
        Task<Account> GetAccountByRefreshTokenAsync(string token);
        Task<bool> CheckIfTokenIsUnique(string token);
        Task SaveAccount(Account account);
        Task UpdateAccount(Account account);
    }
}
