using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineTestingSystem.BLL.Infrastructure;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.BLL.Services;
using OnlineTestingSystem.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        IUserService _userService;
        ICertificateService _certificateService;
        ITestSessionService _testSessionService;
        IMapper _mapper;

        public AccountController(IUserService userService, ICertificateService certificateService, ITestSessionService testSessionService)
        {
            _userService = userService;
            _certificateService = certificateService;
            _testSessionService = testSessionService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestSessionDTO, TestSessionViewModel>()
                .ForMember(u => u.TestName,
                        dal => dal.MapFrom(udto => udto.TestDTO.Name));
                ;
            });
            _mapper = config.CreateMapper();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View("Registration");
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

        [Route("~/Cabinet/Certificates")]
        public ActionResult MyCertificates()
        {
            var email = User.Identity.Name;
            var userId = _userService.GetUserByEmail(email).UserID;  //Remake this
            var certificates = _certificateService.GetCertificatesByUserId(userId);
            return View(certificates);
        }


        [Route("~/Cabinet/Results")]
        public ActionResult MyTestsSessions()
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var userId = user.UserID;  //Remake this
            var sessions = _testSessionService.GetSessionsByUserId(userId);
            var viewModelSessions = _mapper.Map<IEnumerable<TestSessionDTO>, IEnumerable<TestSessionViewModel>>(sessions);
            foreach (var session in viewModelSessions)
            {
                session.TestTime = session.TimeFinish.Subtract(session.TimeStart);
                //session.TestName = 
            }

            return View(viewModelSessions);
        }


        private IUserAppService UserAppService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserAppService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserAppService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Login = model.Login,
                    UserRole = UserRoleDTO.User
                };
                try
                {
                    _userService.CreateUser(userDto);
                }
                catch
                {
                    ViewBag.Error = "Cannot register this user";
                    return View(model);
                }
                OperationDetails operationDetails = await UserAppService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserAppService.SetInitialData(new UserDTO
            {
                Email = "ivashyn.vadym@gmail.com",
                Login = "zevas",
                Password = "123456",
                FirstName = "Vadim",
                LastName = "Ivashyn",
                UserRole = UserRoleDTO.SuperAdmin,
            }, new List<string> { "SuperAdmin", "Admin", "User" });
        }
    }
}