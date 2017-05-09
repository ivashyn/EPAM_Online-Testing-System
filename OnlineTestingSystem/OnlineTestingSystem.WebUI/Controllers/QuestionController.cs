using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
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

        public ActionResult Update(int id)
        {
            var question = _questionService.GetQuestionById(id);
            if (question == null)
                return HttpNotFound();
            ViewBag.Category = new SelectList(_questionCategoryService.GetAllCategories(), "Id", "CategoryName", question.QuestionCategoryId);

            return View(question);
        }

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


        public ActionResult Delete(int id)
        {
            //if (testId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var question = _questionService.GetQuestionById(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionService.DeleteQuestion(id);
            return RedirectToAction("Index", "Home");
        }
    }
}