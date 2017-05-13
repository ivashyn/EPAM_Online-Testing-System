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
            //_userAppService = userAppService;

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

        // GET: Users/?page=3
        public ActionResult Index(int page = 1)
        {
            int totalUsers = _userService.GetAllUsers().Count();
            var users = _userService.GetNUsers(usersPerPage, (page - 1) * usersPerPage);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = usersPerPage, TotalItems = totalUsers };
            var iuvm = new IndexUserViewModel { PageInfo = pageInfo, Users = users };

            return View(iuvm);
        }


        // GET: Users/Delete/5
        [Route("Delete/{userId}")]
        public ActionResult Delete(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "Home", new { @errorText = "The user is not exsist" });
            }

            return View(user);
        }


        [Route("Delete/{userId}")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int userId)
        {
            try
            {
                _userService.DeleteUser(userId);
            }
            catch
            {
                return RedirectToAction("Error", "Home", new { @errorText = "The user is not exsist" });
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: Users/Update/5
        [Route("Update/{userId}")]
        public ActionResult Update(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return RedirectToAction("Error", "Home", new { @errorText = "The user is not exsist" });
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
                var userEmail = User.Identity.Name;
                _userService.UpdateUser(userDTO);
                //_userAppService.ChangeUserRole(userEmail, user.Role);
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