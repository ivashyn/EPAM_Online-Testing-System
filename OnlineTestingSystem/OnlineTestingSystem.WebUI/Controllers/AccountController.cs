using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                user.UserRole = UserRoleDTO.User;
                _userService.CreateUser(user);
            }
            return View(user);

        }
    }
}