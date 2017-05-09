using AutoMapper;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class TestController : Controller
    {
        ITestService _testService;
        ITestSessionService _testSessionService;
        ICertificateService _certificateService;
        IQuestionAnswerService _questionAnswerService;
        IQuestionService _questionService;
        IQuestionCategoryService _questionCategoryService;
        IMapper _mapper;

        public TestController(ITestService testService, IQuestionAnswerService questionAnswerService, IQuestionCategoryService questionCategoryService,
                            IQuestionService questionService, ITestSessionService testSessionService, ICertificateService certificateService)
        {
            _testService = testService;
            _questionAnswerService = questionAnswerService;
            _questionService = questionService;
            _questionCategoryService = questionCategoryService;
            _testSessionService = testSessionService;
            _certificateService = certificateService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionDTO, QuestionViewModel>()
                .ForMember(bgv => bgv.Answers, opt => opt.MapFrom(b => b.QuestionAnswersDTO));
                //.ForMember(b => b.SelectedAnswer, opt => opt.Ignore());
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

            var test = _testService.GetTestById(testId);
            ViewBag.TimeLimit = test.Timelimit;
            ViewBag.TestName = test.Name;

            var testSession = new TestSessionDTO
            {
                IsPassed = false,
                Score = 0,
                TestId = testId,
                TimeStart = DateTime.Now,
                TimeFinish = DateTime.Now,
                UserId = 2  // Remake this later
            };
            _testSessionService.CreateSession(testSession);
            var testSessionId = _testSessionService.GetLastSessionByUserIdAndTestId(2, testId).Id;  //remake THis!!!
            evaluationViewModel.TestSessionId = testSessionId;
            return View(evaluationViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Evaluation(EvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TimeFinish = DateTime.Now;
                List<QuestionAnswerDTO> answers = new List<QuestionAnswerDTO>();
                foreach (var q in model.Questions)
                {
                    var selectedAnswer = _questionAnswerService.GetAnswerById(q.SelectedAnswer);
                    answers.Add(selectedAnswer);
                }
                int testId = model.TestId;
                int score;
                bool isPassed = IsPassed(answers, testId, out score);

                UpdateTestSession(model, isPassed, score, testId);
                if (isPassed)
                    CreateCertificate(testId, score);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private bool IsPassed(IEnumerable<QuestionAnswerDTO> answers, int testId, out int score)
        {
            score = 0;
            foreach (var answer in answers)
            {
                if (answer != null)
                    if (answer.IsRight)
                    {
                        var question = _questionService.GetQuestionById(answer.QuestionId);
                        score += question.Score;
                    }
            }
            var test = _testService.GetTestById(testId);
            if (score >= test.ScoreToPass)
            {
                return true;
            }

            return false;
        }

        private void UpdateTestSession(EvaluationViewModel model, bool isPassed, int score, int testId)
        {
            var testSession = _testSessionService.GetSessionById(model.TestSessionId);
            //var testSession = new TestSessionDTO
            //{
            //    TestId = testId,
            //    Score = score,
            //    IsPassed = isPassed,
            //    TimeStart = model.TimeStart,
            //    TimeFinish = model.TimeFinish,
            //    UserId = 2  //Remake This . . . . . . . . . . . . . .    
            //};
            testSession.Score = score;
            testSession.IsPassed = isPassed;
            testSession.TimeFinish = model.TimeFinish;
            _testSessionService.UpdateSession(testSession);

        }

        private void CreateCertificate(int testId, int score)
        {
            var certificate = new CertificateDTO
            {
                Score = score,
                CertificateNumber = "000013",  //Remake This . . . . . . . . . . . . . . 
                TestDate = DateTime.Now,
                TestId = testId,
                UserId = 2  //Remake This . . . . . . . . . . . . . . 
            };
            _certificateService.CreateCertificate(certificate);
        }


        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestDTO test)
        {
            if (ModelState.IsValid)
            {
                _testService.CreateTest(test);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", test.QuestionCategoryId);
            return View();
        }

        public ActionResult Delete(int testId)
        {
            //if (testId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var test = _testService.GetTestById(testId);
            if (test == null)
            {
                return HttpNotFound();
            }

            return View(test);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int testId)
        {
            _testService.DeleteTest(testId);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Update(int testId)
        {
            var test = _testService.GetTestById(testId);
            if (test == null)
                return HttpNotFound();
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", test.QuestionCategoryId);

            return View(test);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTest(TestDTO test)
        {
            if (ModelState.IsValid)
            {
                _testService.UpdateTest(test);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", test.QuestionCategoryId);

            return View(test);
        }


        //URl /Catalog
        public ActionResult AllTests()
        {
            var allTests = _testService.GetAllTests();
            return View(allTests);
        }

        public ActionResult Test(int id)
        {
            var test = _testService.GetTestById(id);
            ViewBag.NumberOfQuestions = _testService.GetTestQuestions(id).Count();

            return View(test);
        }


    }
}