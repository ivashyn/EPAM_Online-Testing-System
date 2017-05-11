using AutoMapper;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("~/Cabinet/Certificates")]
        public ActionResult MyCertificates()
        {
            var userId = 2;  //Remake this
            var certificates = _certificateService.GetCertificatesByUserId(userId);
            return View(certificates);
        }


        [Route("~/Cabinet/Results")]
        public ActionResult MyTestsSessions()
        {
            var userId = 2;  //Remake this
            var sessions = _testSessionService.GetSessionsByUserId(userId);
            var viewModelSessions = _mapper.Map<IEnumerable<TestSessionDTO>, IEnumerable<TestSessionViewModel>>(sessions);
            foreach (var session in viewModelSessions)
            {
                session.TestTime = session.TimeFinish.Subtract(session.TimeStart);
                //session.TestName = 
            }

            return View(viewModelSessions);
        }
    }
}