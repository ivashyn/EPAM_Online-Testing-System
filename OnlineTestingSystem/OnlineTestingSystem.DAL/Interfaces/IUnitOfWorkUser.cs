using OnlineTestingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Interfaces
{
    public interface IUnitOfWorkUser : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<UserRole> UserRoles { get; }
        void Save();
    }
}
