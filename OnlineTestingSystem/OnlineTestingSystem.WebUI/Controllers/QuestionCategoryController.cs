using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class QuestionCategoryController : Controller
    {
        IQuestionCategoryService _questionCategoryService;

        public QuestionCategoryController(IQuestionCategoryService questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }

        // GET: QuestionCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCategoryDTO catetgoryToCreate)
        {
            if (ModelState.IsValid)
            {
                _questionCategoryService.CreateCategory(catetgoryToCreate);
            }
            return View(catetgoryToCreate);
        }

        public ActionResult Delete(int id)
        {
            //if (testId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var category = _questionCategoryService.GetCagetoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionCategoryService.DeleteCategory(id);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Update(int id)
        {
            var category = _questionCategoryService.GetCagetoryById(id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory(QuestionCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                _questionCategoryService.UpdateCategory(category);
                return RedirectToAction("Index", "Home");
            }

            return View(category);
        }

    }
}