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
    public class QuestionRepository : IRepository<Question>
    {
        TestContext db;

        public QuestionRepository(TestContext context)
        {
            db = context;
        }

        public void Create(Question item)
        {
            db.Questions.Add(item);
        }

        public void Delete(int id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
                db.Questions.Remove(question);
        }

        public IEnumerable<Question> Find(Func<Question, bool> predicate)
        {
            return db.Questions.Where(predicate).ToList();
        }

        public Question Get(int id)
        {
            return db.Questions.Find(id);
        }

        public IEnumerable<Question> GetAll()
        {
            return db.Questions;
        }

        public void Update(Question item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
