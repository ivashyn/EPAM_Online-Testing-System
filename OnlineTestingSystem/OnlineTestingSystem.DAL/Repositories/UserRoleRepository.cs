using OnlineTestingSystem.DAL.Entities;
using OnlineTestingSystem.DAL.EntityFramework;
using OnlineTestingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Repositories
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        TestContext db;

        public UserRoleRepository(TestContext context)
        {
            db = context;
        }

        public void Create(UserRole item)
        {
            db.UserRoles.Add(item);
        }

        public void Delete(int id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole != null)
                db.UserRoles.Remove(userRole);
        }

        public IEnumerable<UserRole> Find(Func<UserRole, bool> predicate)
        {
            return db.UserRoles.Where(predicate).ToList();
        }

        public UserRole Get(int id)
        {
            return db.UserRoles.Find(id);
        }

        public IEnumerable<UserRole> GetAll()
        {
            return db.UserRoles;
        }

        public void Update(UserRole item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
