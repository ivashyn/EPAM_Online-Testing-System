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
                .ForMember(bll => bll.QuestionCategoryDTO, dal => dal.MapFrom(b => b.QuestionCategory))
                .ForMember(bll => bll.QuestionAnswersDTO, dal => dal.MapFrom(b => b.QuestionAnswers));
                cfg.CreateMap<QuestionDTO, Question>()
                 .ForMember(bll => bll.QuestionCategory, dal => dal.MapFrom(b => b.QuestionCategoryDTO))
                .ForMember(bll => bll.QuestionAnswers, dal => dal.MapFrom(b => b.QuestionAnswersDTO));
                ;
                cfg.CreateMap<QuestionCategoryDTO, QuestionCategory>();
                cfg.CreateMap<QuestionCategory, QuestionCategoryDTO>();
                cfg.CreateMap<QuestionAnswerDTO, QuestionAnswer>();
                cfg.CreateMap<QuestionAnswer, QuestionAnswerDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public void CreateQuestion(QuestionDTO question)
        {
            var questionFromDB = GetQuestionByText(question.QuestionText);
            if (questionFromDB != null)
                throw new ValidationException("Sorry, but the question with the same text is exsist", "");

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

        public QuestionDTO GetQuestionByText(string questionText)
        {
            var question = db.Questions.Find(x => x.QuestionText == questionText).FirstOrDefault();
            return _mapper.Map<Question, QuestionDTO>(question);
        }

        public QuestionDTO GetQuestionById(int id)
        {
            var question = db.Questions.Get(id);
            return _mapper.Map<Question, QuestionDTO>(question);
        }

        public void UpdateQuestion(QuestionDTO question)
        {
            var questionDAL = _mapper.Map<QuestionDTO, Question>(question);
            db.Questions.Update(questionDAL);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<QuestionDTO> GetNQuestions(int amountToTake, int amountToSkip)
        {
            var questions = db.Questions.GetAll().Skip(amountToSkip).Take(amountToTake);
            return _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);
        }
    }
}
