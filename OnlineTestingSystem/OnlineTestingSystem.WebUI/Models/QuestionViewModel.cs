using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class QuestionViewModel
    {
        public int Id { set; get; }
        public string QuestionText { set; get; }
        public ICollection<AnswerViewModel> Answers { set; get; }
        [Required]
        public int SelectedAnswer { set; get; }
        public QuestionViewModel()
        {
            Answers = new List<AnswerViewModel>();
        }
    }
}