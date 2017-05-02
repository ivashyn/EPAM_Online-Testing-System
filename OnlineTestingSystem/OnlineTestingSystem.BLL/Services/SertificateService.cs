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
    public class SertificateService : ISertificateService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;
        public SertificateService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Test, TestDTO>();
                cfg.CreateMap<TestDTO, Test>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();

                cfg.CreateMap<Sertificate, SertificateDTO>()
                .ForMember(bgv => bgv.UserDTO, opt => opt.MapFrom(b => b.User))
                .ForMember(bgv => bgv.TestDTO, opt => opt.MapFrom(b => b.Test));
                cfg.CreateMap<SertificateDTO, Sertificate>()
                .ForMember(bgv => bgv.User, opt => opt.MapFrom(b => b.UserDTO)); ;
                
            });
            _mapper = config.CreateMapper();
        }

        public void CreateSertificate(SertificateDTO sertificate)
        {
            var sertificateToAdd = _mapper.Map<SertificateDTO, Sertificate>(sertificate);
            var testFromDb = db.Sertificates.Find(u => u.SertificateNumber == sertificate.SertificateNumber).FirstOrDefault();
            if (testFromDb != null)
                throw new ValidationException("Sorry, but the user with the same Email is already exsist", "");
            db.Sertificates.Create(sertificateToAdd);
            db.Save();
        }

        public void DeleteSertificate(int id)
        {
            var sertificateToDelete = GetSertificateById(id);
            if (sertificateToDelete == null)
                throw new ValidationException("Sorry, but the sertificate doesn't exsist.", "");
            db.Sertificates.Delete(id);
            db.Save();
        }

        public IEnumerable<SertificateDTO> GetAllSertificates()
        {
            var sertificates = db.Sertificates.GetAll();
            return _mapper.Map<IEnumerable<Sertificate>, IEnumerable<SertificateDTO>>(sertificates);
        }

        public SertificateDTO GetSertificateById(int id)
        {
            var sertificate = db.Sertificates.Get(id);
            return _mapper.Map<Sertificate, SertificateDTO>(sertificate);
        }

        public SertificateDTO GetSertificateByNumber(string number)
        {
            var sertificate = db.Sertificates.Find(s => s.SertificateNumber == number).FirstOrDefault();
            return _mapper.Map<Sertificate, SertificateDTO>(sertificate);
        }

        public IEnumerable<SertificateDTO> GetSertificatesByUserId(int userId)
        {
            var sertificates = db.Sertificates.Find(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<Sertificate>, IEnumerable<SertificateDTO>>(sertificates);
        }
    }
}
