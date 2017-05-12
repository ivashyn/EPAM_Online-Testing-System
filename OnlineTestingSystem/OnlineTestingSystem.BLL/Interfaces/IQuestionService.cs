using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IQuestionService : IDisposable
    {
        IEnumerable<QuestionDTO> GetAllQuestions();
        IEnumerable<QuestionDTO> GetNQuestions(int amountToTake, int amountToSkip);
        QuestionDTO GetQuestionById(int id);
        QuestionDTO GetQuestionByText(string questionText);
        void CreateQuestion(QuestionDTO question);
        void DeleteQuestion(int id);
        void UpdateQuestion(QuestionDTO question);
    }
}
