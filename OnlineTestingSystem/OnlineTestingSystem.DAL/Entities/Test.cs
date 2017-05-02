using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public int ScoreToPass { get; set; }

        [Required]
        public byte Timelimit { get; set; }


        public int QuestionCategoryId { get; set; }
        public virtual QuestionCategory QuestionCagegory { get; set; }
        public virtual ICollection<TestSession> TestSessions { get; set; }

        public Test()
        {
            TestSessions = new List<TestSession>();
        }
    }
}
