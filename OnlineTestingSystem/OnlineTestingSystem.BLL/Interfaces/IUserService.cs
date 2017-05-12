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
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDTO> GetAllUsers();
        IEnumerable<UserDTO> GetNUsers(int amountToTake, int amountToSkip);
        UserDTO GetUserByEmail(string email);
        UserDTO GetUserById(int id);
        UserDTO GetUserByLogin(string login);
        IEnumerable<CertificateDTO> GetUsersCertificates(int userId);
        void CreateUser(UserDTO user);
        void DeleteUser(int id);
        void UpdateUser(UserDTO user);
    }
}
