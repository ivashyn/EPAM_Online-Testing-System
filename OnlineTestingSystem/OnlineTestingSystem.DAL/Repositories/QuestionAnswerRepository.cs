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
    public class QuestionAnswerRepository : IRepository<QuestionAnswer>
    {
        TestContext db;

        public QuestionAnswerRepository(TestContext context)
        {
            db = context;
        }

        public void Create(QuestionAnswer item)
        {
            db.QuestionAnswers.Add(item);
        }

        public void Delete(int id)
        {
            QuestionAnswer answer = db.QuestionAnswers.Find(id);
            if (answer != null)
                db.QuestionAnswers.Remove(answer);
        }

        public IEnumerable<QuestionAnswer> Find(Func<QuestionAnswer, bool> predicate)
        {
            return db.QuestionAnswers.Where(predicate).ToList();
        }

        public QuestionAnswer Get(int id)
        {
            return db.QuestionAnswers.Find(id);
        }

        public IEnumerable<QuestionAnswer> GetAll()
        {
            return db.QuestionAnswers;
        }

        public void Update(QuestionAnswer item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
