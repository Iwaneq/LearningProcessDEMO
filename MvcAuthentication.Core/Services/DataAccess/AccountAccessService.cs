using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.Data;
using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services
{
    public class AccountAccessService : BaseDataAccessService
    {
        public AccountAccessService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Account> GetAccountAsync(Credential credentials)
        {
            //Search for username in Database
            var account = await _dataContext.Accounts
                .Where(x => x.Username == credentials.Username)
                .FirstOrDefaultAsync();

            if (account == null) throw new KeyNotFoundException();

            var hasher = new PasswordHasher<Account>();
            var result = hasher.VerifyHashedPassword(account, account.PasswordHash, credentials.Password);

            if (result == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException();
            else return account;
        }

        public async Task SaveAccount(Account account)
        {
            _dataContext.Accounts.Add(account);
            await _dataContext.SaveChangesAsync();
        }
    }
}
