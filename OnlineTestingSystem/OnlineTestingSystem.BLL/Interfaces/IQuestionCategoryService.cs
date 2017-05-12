using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface IQuestionCategoryService : IDisposable
    {
        IEnumerable<QuestionCategoryDTO> GetAllCategories();
        QuestionCategoryDTO GetCagetoryById(int id);
        void CreateCategory(QuestionCategoryDTO category);
        void DeleteCategory(int id);
        void UpdateCategory(QuestionCategoryDTO category);


    }
}
