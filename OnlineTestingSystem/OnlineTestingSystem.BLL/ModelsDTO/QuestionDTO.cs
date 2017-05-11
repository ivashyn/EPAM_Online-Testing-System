using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }

        [StringLength(1500, MinimumLength = 3)]
        [Required]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }
        public int Score { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategoryDTO QuestionCategoryDTO { get; set; }
        public virtual ICollection<QuestionAnswerDTO> QuestionAnswersDTO { get; set; }

        public QuestionDTO()
        {
            QuestionAnswersDTO = new List<QuestionAnswerDTO>();
        }
    }
}
