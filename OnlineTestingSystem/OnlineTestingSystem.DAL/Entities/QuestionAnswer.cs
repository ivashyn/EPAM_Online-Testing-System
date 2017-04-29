using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class QuestionAnswer
    {
        public int Id { get; set; }

        [Required]
        public bool IsRight { get; set; }

        [Required]
        [StringLength(300)]
        public string Answer { get; set; }


        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
