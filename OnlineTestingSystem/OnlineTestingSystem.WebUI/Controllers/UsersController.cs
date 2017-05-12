using AutoMapper;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.WebUI.Models;
using OnlineTestingSystem.WebUI.Models.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    [RoutePrefix("Users")]
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        IUserService _userService;
        IMapper _mapper;
        private int usersPerPage = 10;

        public UsersController(IUserService userService)
        {
            _userService = userService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, UserDTO>().ForMember(u => u.UserRole,
                        dal => dal.MapFrom(udto => (UserRoleDTO)Enum.Parse(typeof(UserRoleDTO), udto.Role.ToString())));
                ;
                cfg.CreateMap<UserDTO, UserViewModel>()
               .ForMember(u => u.Role,
                        dal => dal.MapFrom(udto => udto.UserRole.ToString()));
                //ignore
            });
            _mapper = config.CreateMapper();
        }

        public ActionResult Index(int page = 1)
        {
            int totalUsers = _userService.GetAllUsers().Count();
            var users = _userService.GetNUsers(usersPerPage, (page - 1) * usersPerPage);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = usersPerPage, TotalItems = totalUsers };
            var iuvm = new IndexUserViewModel { PageInfo = pageInfo, Users = users };

            return View(iuvm);
        }


        [Route("Delete/{userId}")]
        public ActionResult Delete(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [Route("Delete/{userId}")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToAction("Index", "Home");
        }


        [Route("Update/{userId}")]
        public ActionResult Update(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return HttpNotFound();
            var roles = new List<string>();
            foreach (UserRoleDTO role in Enum.GetValues(typeof(UserRoleDTO)))
            {
                roles.Add(role.ToString());
            };
            ViewBag.Role = new SelectList(roles, user.UserRole.ToString());
            var userViewModel = _mapper.Map<UserDTO, UserViewModel>(user);
            return View(userViewModel);
        }

        [Route("Update")]
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userPassword = _userService.GetUserById(user.UserID).Password;
                user.Password = userPassword;
                var userDTO = _mapper.Map<UserViewModel, UserDTO>(user);
                _userService.UpdateUser(userDTO);
                return RedirectToAction("Index", "Home");
            }

            var roles = new List<string>();
            foreach (UserRoleDTO role in Enum.GetValues(typeof(UserRoleDTO)))
            {
                roles.Add(role.ToString());
            };
            ViewBag.Role = new SelectList(roles, user.Role);

            return View(user);
        }

    }
}