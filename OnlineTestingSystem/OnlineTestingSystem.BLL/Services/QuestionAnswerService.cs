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
    public class QuestionAnswerService : IQuestionAnswerService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;

        public QuestionAnswerService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionAnswer, QuestionAnswerDTO>()
                .ForMember(bll => bll.QuestionDTO, dal => dal.MapFrom(b => b.Question));
                cfg.CreateMap<QuestionAnswerDTO, QuestionAnswer>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<Question, QuestionDTO>();
            });
            _mapper = config.CreateMapper();
        }


        public void CreateAnswer(QuestionAnswerDTO answer)
        {
            var answerToAdd = _mapper.Map<QuestionAnswerDTO, QuestionAnswer>(answer);
            db.QuestionAnswers.Create(answerToAdd);
            db.Save();
        }

        public void DeleteAnswer(int id)
        {
            var answerToDelete = GetAnswerById(id);
            if (answerToDelete == null)
                throw new ValidationException("Sorry, but the answer doesn't exsist.", "");
            db.QuestionAnswers.Delete(id);
            db.Save();
        }

        public IEnumerable<QuestionAnswerDTO> GetAllAnswers()
        {
            var answers = db.QuestionAnswers.GetAll().ToList();
            return _mapper.Map<IEnumerable<QuestionAnswer>, IEnumerable<QuestionAnswerDTO>>(answers);
        }

        public QuestionAnswerDTO GetAnswerById(int id)
        {
            var answer = db.QuestionAnswers.Get(id);
            return _mapper.Map<QuestionAnswer, QuestionAnswerDTO>(answer);
        }

        public IEnumerable<QuestionAnswerDTO> GetAnswersByQuestionId(int questionId)
        {
            var answers = db.QuestionAnswers.Find(q => q.QuestionId == questionId);
            return _mapper.Map<IEnumerable<QuestionAnswer>, IEnumerable<QuestionAnswerDTO>>(answers);
        }
    }
}
