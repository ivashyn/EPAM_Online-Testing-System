using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers(); //pagination
            return View(users);
        }

        //CRUD

        public ActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Update(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return HttpNotFound();
            var roles = new List<string>();
            foreach (UserRoleDTO role in Enum.GetValues(typeof(UserRoleDTO)))
            {
                roles.Add(role.ToString());
            };
            ViewBag.Role = new SelectList(roles, user.UserRole.ToString());

            return View(user);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var userPassword = _userService.GetUserById(user.UserID).Password;
                user.Password = userPassword;
                _userService.UpdateUser(user);
                return RedirectToAction("Index", "Home");
            }

            var roles = new List<string>();
            foreach (UserRoleDTO role in Enum.GetValues(typeof(UserRoleDTO)))
            {
                roles.Add(role.ToString());
            };
            ViewBag.Role = new SelectList(roles, user.UserRole.ToString());

            return View(user);
        }

    }
}