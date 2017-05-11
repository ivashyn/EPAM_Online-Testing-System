using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ScoreToPass { get; set; }
        public byte Timelimit { get; set; }

        public int QuestionCategoryId { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}