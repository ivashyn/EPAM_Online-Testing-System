using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [RoutePrefix("Question")]
    public class QuestionController : Controller
    {
        IQuestionService _questionService;
        IQuestionAnswerService _questionAnswerService;
        IQuestionCategoryService _questionCategoryService;

        public QuestionController(IQuestionService questionService, IQuestionAnswerService questionAnswerService, IQuestionCategoryService questionCategoryService)
        {
            _questionService = questionService;
            _questionAnswerService = questionAnswerService;
            _questionCategoryService = questionCategoryService;
        }

        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName");
            return View(new QuestionDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionDTO questionToCreate)
        {
            if (ModelState.IsValid)
            {
                _questionService.CreateQuestion(questionToCreate);
                var questionId = _questionService.GetQuestionByText(questionToCreate.QuestionText).Id;
                foreach (var answer in questionToCreate.QuestionAnswersDTO)
                {
                    answer.QuestionId = questionId;
                    _questionAnswerService.CreateAnswer(answer);
                }
            }
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", questionToCreate.QuestionCategoryId);
            return View(questionToCreate);
        }


        [Route("Update/{questionId}")]
        public ActionResult Update(int questionId)
        {
            var question = _questionService.GetQuestionById(questionId);
            if (question == null)
                return HttpNotFound();
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", question.QuestionCategoryId);

            return View(question);
        }

        [Route("Update")]
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(QuestionDTO question)
        {
            if (ModelState.IsValid)
            {
                _questionService.UpdateQuestion(question);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", question.QuestionCategoryId);
            return View(question);
        }

        [Route("Delete/{questionId}")]
        public ActionResult Delete(int questionId)
        {
            var question = _questionService.GetQuestionById(questionId);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }


        [Route("Delete/{questionId}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int questionId)
        {
            _questionService.DeleteQuestion(questionId);
            return RedirectToAction("Index", "Home");
        }
    }
}