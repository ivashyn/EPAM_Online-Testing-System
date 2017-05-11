using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }

        [StringLength(1500, MinimumLength = 3)]
        [Required]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int QuestionCategoryId { get; set; }
        public virtual QuestionCategory QuestionCategory { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public Question()
        {
            QuestionAnswers = new List<QuestionAnswer>();
        }

    }
}
