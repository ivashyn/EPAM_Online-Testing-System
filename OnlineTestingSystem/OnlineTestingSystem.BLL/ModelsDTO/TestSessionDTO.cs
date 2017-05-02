using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class TestSessionDTO
    {
        public int Id { get; set; }
        public bool IsPassed { get; set; }
        public int Score { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }

        public int UserId { get; set; }
        public int TestId { get; set; }
        public UserDTO UserDTO { get; set; }
        public TestDTO TestDTO { get; set; }
    }
}
