using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class QuestionCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string CategoryName { get; set; }

        public virtual ICollection<QuestionDTO> QuestionsDTO { get; set; }
        public virtual ICollection<TestDTO> TestsDTO { get; set; }  //maybe 1:1?

        public QuestionCategoryDTO()
        {
            QuestionsDTO = new List<QuestionDTO>();
            TestsDTO = new List<TestDTO>();
        }
    }
}
