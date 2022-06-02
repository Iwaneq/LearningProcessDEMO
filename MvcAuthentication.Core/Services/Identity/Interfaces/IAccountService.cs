using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services.Identity.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(Credential credentials);
    }
}
