using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface ITestService : IDisposable
    {
        IEnumerable<TestDTO> GetAllTests();
        IEnumerable<TestDTO> GetNTests(int amountToTake, int amountToSkip);
        TestDTO GetTestById(int id);
        void CreateTest(TestDTO test);
        void UpdateTest(TestDTO test);
        void DeleteTest(int id);
        IEnumerable<QuestionDTO> GetTestQuestions(int testId);
        IEnumerable<QuestionAnswerDTO> GetTestAnswers(int testId);
        int GetAmountOfQuestions(int testId);
    }
}
