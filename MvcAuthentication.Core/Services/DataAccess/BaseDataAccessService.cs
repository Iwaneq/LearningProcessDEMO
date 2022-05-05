using MvcAuthentication.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Services
{
    public abstract class BaseDataAccessService
    {
        protected DataContext _dataContext;

        public BaseDataAccessService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
