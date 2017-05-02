using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<QuestionDTO> GetAllQuestions();
        QuestionDTO GetQuestionById(int id);
        void CreateQuestion(QuestionDTO question);
        void DeleteQuestion(int id);
    }
}
