using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineTestingSystem.BLL.Infrastructure;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.BLL.Services;
using OnlineTestingSystem.WebUI.Models;
using OnlineTestingSystem.WebUI.Models.PaginationModels;
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
        private int certificatesPerPage = 10;
        private int sessionsPerPage = 10;

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

        //GET: Cabinet/Certificates
        [Authorize]
        [Route("~/Cabinet/Certificates")]
        public ActionResult MyCertificates(int page = 1)
        {
            var email = User.Identity.Name;
            var userId = _userService.GetUserByEmail(email).UserID;
            var allUserCertificates = _certificateService.GetCertificatesByUserId(userId);

            int totalCertificates = allUserCertificates.Count();
            var certificates = _certificateService.GetNCertificatesByUserId(userId, certificatesPerPage, (page - 1) * certificatesPerPage);

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = certificatesPerPage, TotalItems = totalCertificates };
            var icvm = new IndexCertificatesViewModel { PageInfo = pageInfo, Certificates = certificates };

            return View(icvm);
        }


        //GET: Cabinet/Results
        [Route("~/Cabinet/Results")]
        [Authorize]
        public ActionResult MyTestsSessions(int page = 1)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var userId = user.UserID;
            var allUserSessions = _testSessionService.GetSessionsByUserId(userId);
            var allViewModelSessions = _mapper.Map<IEnumerable<TestSessionDTO>, IEnumerable<TestSessionViewModel>>(allUserSessions);

            int totalSessions = allViewModelSessions.Count();
            var sessions = _testSessionService.GetNSessionsByUserId(userId, sessionsPerPage, (page - 1) * sessionsPerPage);
            var viewModelSessions = _mapper.Map<IEnumerable<TestSessionDTO>, IEnumerable<TestSessionViewModel>>(sessions);
            foreach (var session in viewModelSessions)
            {
                session.TestTime = session.TimeFinish.Subtract(session.TimeStart);
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = sessionsPerPage, TotalItems = totalSessions };
            var icvm = new IndexSessionViewModel { PageInfo = pageInfo, Sessions = viewModelSessions };

            return View(icvm);
        }

        //GET: Cabinet
        [Authorize]
        [Route("~/Cabinet")]
        public ActionResult Cabinet()
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            return View(user);
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

        //GET: Account/Login
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
                    ModelState.AddModelError("", "Invalid Login / Password.");
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

        //GET: Account/Logout
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //GET: Account/Registration
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterModel model)
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