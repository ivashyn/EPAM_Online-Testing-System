using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUserByEmail(string email);
        UserDTO GetUserById(int id);
        IEnumerable<SertificateDTO> GetUsersSertificate(int userId);
        void CreateUser(UserDTO user);
        void DeleteUser(int id);
    }
}
