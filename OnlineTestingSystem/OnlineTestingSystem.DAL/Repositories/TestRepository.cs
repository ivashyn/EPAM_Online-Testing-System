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
    public class TestRepository : IRepository<Test>
    {
        TestContext db;

        public TestRepository(TestContext context)
        {
            db = context;
        }

        public void Create(Test item)
        {
            db.Tests.Add(item);
        }

        public void Delete(int id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
                db.Tests.Remove(test);
        }

        public IEnumerable<Test> Find(Func<Test, bool> predicate)
        {
            return db.Tests.Where(predicate).ToList();
        }

        public Test Get(int id)
        {
            return db.Tests.Find(id);
        }

        public IEnumerable<Test> GetAll()
        {
            return db.Tests;
        }

        public void Update(Test item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
