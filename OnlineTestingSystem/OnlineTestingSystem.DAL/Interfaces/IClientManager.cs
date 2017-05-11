using OnlineTestingSystem.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(UserProfile item);
    }
}
