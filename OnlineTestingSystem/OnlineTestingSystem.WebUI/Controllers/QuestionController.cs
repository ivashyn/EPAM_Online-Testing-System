using OnlineTestingSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionService _questionService;
        IQuestionAnswerService _questionAnswerService;

        public QuestionController(IQuestionService questionService, IQuestionAnswerService questionAnswerService)
        {
            _questionService = questionService;
            _questionAnswerService = questionAnswerService;
        }
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}