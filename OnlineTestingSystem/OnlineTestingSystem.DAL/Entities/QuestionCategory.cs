using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class QuestionCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string CategoryName { get; set; }


        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Test> Tests { get; set; }  //maybe 1:1?

        public QuestionCategory()
        {
            Questions = new List<Question>();
            Tests = new List<Test>();
        }
    }
}
