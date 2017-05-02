using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestingSystem.BLL.ModelsDTO;
using AutoMapper;
using OnlineTestingSystem.DAL.Entities;
using OnlineTestingSystem.BLL.Infrastructure;

namespace OnlineTestingSystem.BLL.Services
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;
        public QuestionCategoryService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionCategory, QuestionCategoryDTO>()
                .ForMember(bgv => bgv.QuestionsDTO, opt => opt.MapFrom(b => b.Questions))
                .ForMember(bgv => bgv.TestsDTO, opt => opt.MapFrom(b => b.Tests));
                cfg.CreateMap<QuestionCategoryDTO, QuestionCategory>();
                cfg.CreateMap<Question, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<Test, TestDTO>();
                cfg.CreateMap<TestDTO, Test>();
            });
            _mapper = config.CreateMapper();
        }

        public void CreateCategory(QuestionCategoryDTO category)
        {
            var categoryToAdd = _mapper.Map<QuestionCategoryDTO, QuestionCategory>(category);
            db.QuestionCategories.Create(categoryToAdd);
            db.Save();
        }

        public void DeleteCategory(int id)
        {
            var categoryToDelete = GetCagetoryById(id);
            if (categoryToDelete == null)
                throw new ValidationException("Sorry, but the category doesn't exsist.", "");
            db.QuestionCategories.Delete(id);
            db.Save();
        }

        public IEnumerable<QuestionCategoryDTO> GetAllCategories()
        {
            var categories = db.QuestionCategories.GetAll().ToList();
            return _mapper.Map<IEnumerable<QuestionCategory>, IEnumerable<QuestionCategoryDTO>>(categories);
        }

        public QuestionCategoryDTO GetCagetoryById(int id)
        {
            /*delete*/
            var category = db.QuestionCategories.Get(id);
            return _mapper.Map<QuestionCategory, QuestionCategoryDTO>(category);
        }
    }
}
