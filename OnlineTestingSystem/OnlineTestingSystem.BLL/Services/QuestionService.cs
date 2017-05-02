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
    public class QuestionService : IQuestionService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;

        public QuestionService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionDTO>()
                .ForMember(bgv => bgv.QuestionCategoryDTO, opt => opt.MapFrom(b => b.QuestionCategory))
                .ForMember(bgv => bgv.QuestionAnswersDTO, opt => opt.MapFrom(b => b.QuestionAnswers));
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<QuestionCategoryDTO, QuestionCategory>();
                cfg.CreateMap<QuestionCategory, QuestionCategoryDTO>();
                cfg.CreateMap<QuestionAnswerDTO, QuestionAnswer>();
                cfg.CreateMap<QuestionAnswer, QuestionAnswerDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public void CreateQuestion(QuestionDTO question)
        {
            var questionToAdd = _mapper.Map<QuestionDTO, Question>(question);
            db.Questions.Create(questionToAdd);            
            /*categoryId 123*/
            db.Save();

        }

        public void DeleteQuestion(int id)
        {
            var questionToDelete = GetQuestionById(id);
            if (questionToDelete == null)
                throw new ValidationException("Sorry, but the question doesn't exsist.", "");
            db.Questions.Delete(id);
            db.Save();
        }

        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            var questions = db.Questions.GetAll();
            return _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);
        }

        public QuestionDTO GetQuestionById(int id)
        {
            var question = db.Questions.Get(id);
            return _mapper.Map<Question, QuestionDTO>(question);
        }
    }
}
