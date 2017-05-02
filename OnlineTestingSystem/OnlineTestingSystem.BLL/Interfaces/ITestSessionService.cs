using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface ITestSessionService
    {
        IEnumerable<TestSessionDTO> GetAllSessions();
        TestSessionDTO GetSessionById(int id);
        void CreateSession(TestSessionDTO session);
        void DeleteSession(int id);
    }
}
