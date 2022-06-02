﻿using MvcAuthentication.Core.User;
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
        Task SaveAccount(Account account);
    }
}
