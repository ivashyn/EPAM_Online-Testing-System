using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class AnswerViewModel
    {
        public int Id { set; get; }
        public string Answer { set; get; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
    }
}