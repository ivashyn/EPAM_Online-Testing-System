using OnlineTestingSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestingSystem.BLL.ModelsDTO;
using OnlineTestingSystem.DAL.Interfaces;
using AutoMapper;
using OnlineTestingSystem.DAL.Entities;
using OnlineTestingSystem.BLL.Infrastructure;

namespace OnlineTestingSystem.BLL.Services
{
    public class TestSessionService : ITestSessionService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;
        public TestSessionService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestSession, TestSessionDTO>()
                .ForMember(bll => bll.UserDTO, dal => dal.MapFrom(b => b.User))
                .ForMember(bll => bll.TestDTO, dal => dal.MapFrom(b => b.Test));
                cfg.CreateMap<TestSessionDTO, TestSession>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Test, TestDTO>();
                cfg.CreateMap<TestDTO, Test>();
            });
            _mapper = config.CreateMapper();
        }

        public void CreateSession(TestSessionDTO session)
        {
            var sessionToAdd = _mapper.Map<TestSessionDTO, TestSession>(session);
            db.TestSessions.Create(sessionToAdd);
            db.Save();
        }

        public void DeleteSession(int id)
        {
            var sessionToDelete = GetSessionById(id);
            if (sessionToDelete == null)
                throw new ValidationException("Sorry, but the session doesn't exsist.", "");
            db.TestSessions.Delete(id);
            db.Save();
        }

        public IEnumerable<TestSessionDTO> GetAllSessions()
        {
            var session = db.TestSessions.GetAll();
            return _mapper.Map<IEnumerable<TestSession>, IEnumerable<TestSessionDTO>>(session);

        }

        public TestSessionDTO GetLastSessionByUserIdAndTestId(int userId, int testId)
        {
            var session = db.TestSessions.Find(x => x.UserId == userId && x.TestId == testId).LastOrDefault();
            return _mapper.Map<TestSession, TestSessionDTO>(session);
        }

        public TestSessionDTO GetSessionById(int id)
        {
            var session = db.TestSessions.Get(id);
            return _mapper.Map<TestSession, TestSessionDTO>(session);
        }

        public IEnumerable<TestSessionDTO> GetSessionsByUserId(int userId)
        {
            var sessions = db.TestSessions.Find(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<TestSession>, IEnumerable<TestSessionDTO>>(sessions);
        }

        public void UpdateSession(TestSessionDTO session)
        {
            var testSession = _mapper.Map<TestSessionDTO, TestSession>(session);
            db.TestSessions.Update(testSession);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<TestSessionDTO> GetNSessionsByUserId(int userId, int amountToTake, int amountToSkip)
        {
            var sessions = GetSessionsByUserId(userId).Skip(amountToSkip).Take(amountToTake);
            return sessions;
        }
    }
}
