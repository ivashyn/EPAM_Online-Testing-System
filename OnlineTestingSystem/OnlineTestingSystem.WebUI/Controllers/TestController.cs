using AutoMapper;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class TestController : Controller
    {
        ITestService _testService;
        IQuestionAnswerService _questionAnswerService;
        IQuestionService _questionService;
        IMapper _mapper;

        public TestController(ITestService testService, IQuestionAnswerService questionAnswerService, IQuestionService questionService)
        {
            _testService = testService;
            _questionAnswerService = questionAnswerService;
            _questionService = questionService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionDTO, QuestionViewModel>()
                .ForMember(bgv => bgv.Answers, opt => opt.MapFrom(b => b.QuestionAnswersDTO))
                .ForMember(b => b.SelectedAnswer, opt => opt.Ignore());
                cfg.CreateMap<QuestionAnswerDTO, AnswerViewModel>()
                .ForMember(b => b.QuestionViewMode, opt => opt.MapFrom(b => b.QuestionDTO));

            });
            _mapper = config.CreateMapper();
        }

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Evaluation(int testId)
        {
            var testQuestions = _testService.GetTestQuestions(testId);
            var questions = _mapper.Map<IEnumerable<QuestionDTO>, IEnumerable<QuestionViewModel>>(testQuestions);
            var evaluationViewModel = new EvaluationViewModel(testId);
            foreach (var item in questions)
            {
                evaluationViewModel.Questions.Add(item);
            }

            ViewBag.TimeLimit = _testService.GetTestById(testId).Timelimit;
            return View(evaluationViewModel);
        }


        [HttpPost]
        public ActionResult Evaluation(EvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<QuestionAnswerDTO> answers = new List<QuestionAnswerDTO>();
                foreach (var q in model.Questions)
                {
                    var selectedAnswer = _questionAnswerService.GetAnswerById(q.SelectedAnswer);
                    answers.Add(selectedAnswer);
                }
                int testId = model.TestId;
                bool isPassed = IsPassed(answers, testId);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private bool IsPassed(IEnumerable<QuestionAnswerDTO> answers, int testId)
        {
            int score = 0;
            foreach (var answer in answers)
            {
                if (answer.IsRight)
                {
                    var question = _questionService.GetQuestionById(answer.QuestionId);
                    score += question.Score;
                }
            }
            var test = _testService.GetTestById(testId);
            if (score >= test.ScoreToPass)
            {
                //TODO: Create TestSession, Sertificate
                return true;
            }

            return false;
        }

        private void CreateTestSession()
        {

        }
        public ActionResult AllTests()
        {
            var allTests = _testService.GetAllTests();
            return View(allTests);
        }

        public ActionResult Test(int id)
        {
            var test = _testService.GetTestById(id);

            return View(test);
        }


    }
}