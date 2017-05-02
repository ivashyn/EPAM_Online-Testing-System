using Ninject;
using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IQuestionCategoryService>().To<QuestionCategoryService>();
            kernel.Bind<IQuestionService>().To<QuestionService>();
            kernel.Bind<IQuestionAnswerService>().To<QuestionAnswerService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<ISertificateService>().To<SertificateService>();
            kernel.Bind<ITestSessionService>().To<TestSessionService>();
        }
    }
}