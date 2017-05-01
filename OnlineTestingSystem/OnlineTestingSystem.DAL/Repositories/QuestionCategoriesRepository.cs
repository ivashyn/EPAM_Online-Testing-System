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
    public class QuestionCategoriesRepository : IRepository<QuestionCategory>
    {
        TestContext db;

        public QuestionCategoriesRepository(TestContext context)
        {
            db = context;
        }

        public void Create(QuestionCategory item)
        {
            db.QuestionCategories.Add(item);
        }

        public void Delete(int id)
        {
            QuestionCategory category = db.QuestionCategories.Find(id);
            if (category != null)
                db.QuestionCategories.Remove(category);
        }

        public IEnumerable<QuestionCategory> Find(Func<QuestionCategory, bool> predicate)
        {
            return db.QuestionCategories.Where(predicate).ToList();
        }

        public QuestionCategory Get(int id)
        {
            return db.QuestionCategories.Find(id);
        }

        public IEnumerable<QuestionCategory> GetAll()
        {
            return db.QuestionCategories;
        }

        public void Update(QuestionCategory item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
