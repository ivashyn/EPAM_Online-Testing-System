using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserAppService CreateUserService(string connection)
        {
            return new UserAppService(new IdentityUnitOfWork(connection));
        }
    }
}
