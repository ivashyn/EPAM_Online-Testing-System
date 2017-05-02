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
    public class TestService : ITestService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;

        public TestService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Test, TestDTO>()
                .ForMember(bgv => bgv.QuestionCagegoryDTO, opt => opt.MapFrom(b => b.QuestionCagegory))
                .ForMember(bgv => bgv.TestSessionsDTO, opt => opt.MapFrom(b => b.TestSessions));
                cfg.CreateMap<TestDTO, Test>();
                cfg.CreateMap<QuestionCategory, QuestionCategoryDTO>();
                cfg.CreateMap<QuestionCategoryDTO, QuestionCategory>();
                cfg.CreateMap<TestSession, TestSessionDTO>();
                cfg.CreateMap<TestSessionDTO, TestSession>();
            });
            _mapper = config.CreateMapper();

        }

        public void CreateTest(TestDTO test)
        {
            var testToAdd = _mapper.Map<TestDTO, Test>(test);
            db.Tests.Create(testToAdd);
            db.Save();
        }

        public void DeleteTest(int id)
        {
            var testToDelete = GetTestById(id);
            if (testToDelete == null)
                throw new ValidationException("Sorry, but the test doesn't exsist.", "");
            db.Tests.Delete(id);
            db.Save();
        }

        public IEnumerable<TestDTO> GetAllTests()
        {
            var tests = db.Tests.GetAll();
            return _mapper.Map<IEnumerable<Test>, IEnumerable<TestDTO>>(tests);
        }

        public TestDTO GetTestById(int id)
        {
            var test = db.Tests.Get(id);
            return _mapper.Map<Test, TestDTO>(test);
        }
    }
}
