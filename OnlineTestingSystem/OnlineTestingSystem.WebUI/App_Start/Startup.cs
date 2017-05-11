using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.Services;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: OwinStartup(typeof(OnlineTestingSystem.WebUI.App_Start.Startup))]

namespace OnlineTestingSystem.WebUI.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserAppService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserAppService CreateUserService()
        {
            return serviceCreator.CreateUserService("IdentityDb");
        }
    }
}