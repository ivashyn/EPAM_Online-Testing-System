using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IQuestionAnswerService : IDisposable
    {
        IEnumerable<QuestionAnswerDTO> GetAllAnswers();
        IEnumerable<QuestionAnswerDTO> GetAnswersByQuestionId(int questionId);
        QuestionAnswerDTO GetAnswerById(int id);
        void CreateAnswer(QuestionAnswerDTO answer);
        void DeleteAnswer(int id);
    }
}
