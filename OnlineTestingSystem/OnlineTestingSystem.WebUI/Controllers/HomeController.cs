using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IQuestionCategoryService _questionCategoryService;
        IQuestionService _questionService;
        IUserService _userService;
        ICertificateService _sertificateService;
        ITestService _testService;
        ITestSessionService _testSessionService;
        IQuestionAnswerService _questionAnswerService;

        public HomeController(IQuestionCategoryService qcservice, IQuestionService qService, IUserService uService,
                                ICertificateService sService, ITestService tService, ITestSessionService tsService, IQuestionAnswerService qaService)
        {
            _questionCategoryService = qcservice;
            _questionService = qService;
            _userService = uService;
            _sertificateService = sService;
            _testService = tService;
            _testSessionService = tsService;
            _questionAnswerService = qaService;
        }


        public ActionResult Index()
        {
            //var question = _questionService.GetQuestionById(3);
            //var questionAnswer = _questionAnswerService.GetAnswerById(2);
            //var category = _questionCategoryService.GetCagetoryById(1);
            //var test = _testService.GetTestById(2);
            //var testSession = _testSessionService.GetSessionById(2);
            //var sertificate = _sertificateService.GetSertificateById(4);
            //var user = _userService.GetUserById(2);

            //var s = _questionCategoryService.GetAllCategories().ToList();
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string errorText = "You Requested the page that is no longer There.")
        {
            ViewBag.ErrorText = errorText;
            return View();
        }
    }
}