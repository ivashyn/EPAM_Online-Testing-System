using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface ITestSessionService : IDisposable
    {
        IEnumerable<TestSessionDTO> GetAllSessions();
        IEnumerable<TestSessionDTO> GetSessionsByUserId(int userId);
        IEnumerable<TestSessionDTO> GetNSessionsByUserId(int userId, int amountToTake, int amountToSkip);
        TestSessionDTO GetSessionById(int id);
        TestSessionDTO GetLastSessionByUserIdAndTestId(int userId, int testId);
        void CreateSession(TestSessionDTO session);
        void DeleteSession(int id);
        void UpdateSession(TestSessionDTO session);
    }
}
