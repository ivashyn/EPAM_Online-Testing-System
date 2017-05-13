using OnlineTestingSystem.BLL.Infrastructure;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        //void ChangeUserRole(string userEmail, string role);
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
