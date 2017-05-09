using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class EvaluationViewModel
    {
        public int TestId { get; set; }
        public List<QuestionViewModel> Questions { set; get; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public int TestSessionId { get; set; }
        public EvaluationViewModel(int testId)
        {
            TestId = testId;
            Questions = new List<QuestionViewModel>();
        }
        public EvaluationViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }
    }
}