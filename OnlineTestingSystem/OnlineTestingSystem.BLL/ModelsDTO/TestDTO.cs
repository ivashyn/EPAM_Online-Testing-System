using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public int ScoreToPass { get; set; }        
        public byte Timelimit { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategoryDTO QuestionCagegoryDTO { get; set; }
        public virtual ICollection<TestSessionDTO> TestSessionsDTO { get; set; }

        public TestDTO()
        {
            TestSessionsDTO = new List<TestSessionDTO>();
        }
    }
}
