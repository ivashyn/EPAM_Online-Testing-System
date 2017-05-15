using OnlineTestingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Interfaces
{
    public interface IUnitOfWorkTest : IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<TestSession> TestSessions { get; }
        IRepository<Certificate> Certificates { get; }
        IRepository<Question> Questions { get;  }
        IRepository<QuestionAnswer> QuestionAnswers { get; }
        IRepository<QuestionCategory> QuestionCategories { get; }
        void Save();
    }
}
