using Ninject.Modules;
using OnlineTestingSystem.DAL.Interfaces;
using OnlineTestingSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWorkTest>().To<UnitOfWorkTest>().WithConstructorArgument(connectionString);
            Bind<IUnitOfWorkUser>().To<UnitOfWorkUser>().WithConstructorArgument(connectionString);
        }
    }
}
