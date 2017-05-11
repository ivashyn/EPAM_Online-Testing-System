using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    [RoutePrefix("QuestionCategory")]
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

        [Route("Delete/{categoryId}")]
        public ActionResult Delete(int categoryId)
        {
            var category = _questionCategoryService.GetCagetoryById(categoryId);
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
            _questionCategoryService.DeleteCategory(id); //exeption 
            return RedirectToAction("Index", "Home");
        }


        [Route("Update/{categoryId}")]
        public ActionResult Update(int categoryId)
        {
            var category = _questionCategoryService.GetCagetoryById(categoryId);
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