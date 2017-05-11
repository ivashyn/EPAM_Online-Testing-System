using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }

        public bool IsRight { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 2)]
        public string Answer { get; set; }

        public int QuestionId { get; set; }
        public QuestionDTO QuestionDTO { get; set; }
    }
}
