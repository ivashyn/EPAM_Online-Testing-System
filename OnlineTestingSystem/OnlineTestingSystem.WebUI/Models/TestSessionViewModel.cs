using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class TestSessionViewModel
    {
        public int Id { get; set; }
        public bool IsPassed { get; set; }
        public int Score { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public TimeSpan TestTime { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
    }
}