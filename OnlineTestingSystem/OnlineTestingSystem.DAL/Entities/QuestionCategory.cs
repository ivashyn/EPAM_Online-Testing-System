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
        [StringLength(40)]
        public string CategoryName { get; set; }


        public int TestId { get; set; }
        public virtual Test Test { get; set; }  //maybe 1:1?
        public virtual ICollection<Question> Questions { get; set; }
        

        public QuestionCategory()
        {
            Questions = new List<Question>();
            //Tests = new List<Test>();
        }
    }
}
