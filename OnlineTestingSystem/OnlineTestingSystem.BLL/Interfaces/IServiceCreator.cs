using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserAppService CreateUserService(string connection);
    }
}
