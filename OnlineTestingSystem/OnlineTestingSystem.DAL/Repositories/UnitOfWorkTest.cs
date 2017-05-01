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
    class UnitOfWorkTest : IUnitOfWorkTest
    {
        TestContext db = new TestContext();
        private QuestionCategoriesRepository questionCategoryRepository;
        private QuestionRepository questionRepository;
        private QuestionAnswerRepository questionAnswerRepository;
        private TestRepository testRepository;
        private TestSessionRepository testSessionRepository;
        private SertificateRepository sertificateRepository;

        public IRepository<Test> Tests
        {
            get
            {
                if (testRepository == null)
                    testRepository = new TestRepository(db);
                return testRepository;
            }
        }

        public IRepository<TestSession> TestSessions
        {
            get
            {
                if (testSessionRepository == null)
                    testSessionRepository = new TestSessionRepository(db);
                return testSessionRepository;
            }
        }

        public IRepository<Sertificate> Sertificates
        {
            get
            {
                if (sertificateRepository == null)
                    sertificateRepository = new SertificateRepository(db);
                return sertificateRepository;
            }
        }

        public IRepository<QuestionAnswer> QuestionAnswers
        {
            get
            {
                if (questionAnswerRepository == null)
                    questionAnswerRepository = new QuestionAnswerRepository(db);
                return questionAnswerRepository;
            }
        }

        public IRepository<QuestionCategory> QuestionCategories
        {
            get
            {
                if (questionCategoryRepository == null)
                    questionCategoryRepository = new QuestionCategoriesRepository(db);
                return questionCategoryRepository;
            }
        }

        public IRepository<Question> Questions
        {
            get
            {
                if (questionRepository == null)
                    questionRepository = new QuestionRepository(db);
                return questionRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
