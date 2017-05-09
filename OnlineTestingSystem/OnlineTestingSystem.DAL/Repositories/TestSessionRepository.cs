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
    public class TestSessionRepository : IRepository<TestSession>
    {
        TestContext db;

        public TestSessionRepository(TestContext context)
        {
            db = context;
        }

        public void Create(TestSession item)
        {
            db.TestSessions.Add(item);
        }

        public void Delete(int id)
        {
            TestSession session = db.TestSessions.Find(id);
            if (session != null)
                db.TestSessions.Remove(session);
        }

        public IEnumerable<TestSession> Find(Func<TestSession, bool> predicate)
        {
            return db.TestSessions.Where(predicate).ToList();
        }

        public TestSession Get(int id)
        {
            return db.TestSessions.Find(id);
        }

        public IEnumerable<TestSession> GetAll()
        {
            return db.TestSessions;
        }

        public void Update(TestSession item)
        {
            var modelExsist = db.TestSessions.Find(item.Id);
            if (modelExsist != null)
                db.Entry(modelExsist).State = System.Data.Entity.EntityState.Detached;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
