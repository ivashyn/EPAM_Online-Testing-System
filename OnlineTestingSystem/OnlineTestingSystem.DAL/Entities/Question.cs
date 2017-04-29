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

        [Required]
        [StringLength(1500)]
        public string QuestionText { get; set; }

        [Required]
        public int Score { get; set; }


        public int QuestionCategoryId { get; set; }
        public virtual QuestionCategory QuestionCategory { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public Question()
        {
            QuestionAnswers = new List<QuestionAnswer>();
        }

    }
}
