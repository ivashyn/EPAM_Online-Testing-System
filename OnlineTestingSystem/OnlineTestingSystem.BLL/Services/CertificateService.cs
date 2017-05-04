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
    public class CertificateService : ICertificateService
    {
        IUnitOfWorkTest db;
        IMapper _mapper;
        public CertificateService(IUnitOfWorkTest uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Test, TestDTO>();
                cfg.CreateMap<TestDTO, Test>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();

                cfg.CreateMap<Certificate, CertificateDTO>()
                .ForMember(bgv => bgv.UserDTO, opt => opt.MapFrom(b => b.User))
                .ForMember(bgv => bgv.TestDTO, opt => opt.MapFrom(b => b.Test));
                cfg.CreateMap<CertificateDTO, Certificate>()
                .ForMember(bgv => bgv.User, opt => opt.MapFrom(b => b.UserDTO)); ;
                
            });
            _mapper = config.CreateMapper();
        }

        public void CreateCertificate(CertificateDTO sertificate)
        {
            var sertificateToAdd = _mapper.Map<CertificateDTO, Certificate>(sertificate);
            var testFromDb = db.Certificates.Find(u => u.CertificateNumber == sertificate.SertificateNumber).FirstOrDefault();
            if (testFromDb != null)
                throw new ValidationException("Sorry, but the user with the same Email is already exsist", "");
            db.Certificates.Create(sertificateToAdd);
            db.Save();
        }

        public void DeleteCertificate(int id)
        {
            var sertificateToDelete = GetCertificateById(id);
            if (sertificateToDelete == null)
                throw new ValidationException("Sorry, but the sertificate doesn't exsist.", "");
            db.Certificates.Delete(id);
            db.Save();
        }

        public IEnumerable<CertificateDTO> GetAllCertificates()
        {
            var sertificates = db.Certificates.GetAll();
            return _mapper.Map<IEnumerable<Certificate>, IEnumerable<CertificateDTO>>(sertificates);
        }

        public CertificateDTO GetCertificateById(int id)
        {
            var sertificate = db.Certificates.Get(id);
            return _mapper.Map<Certificate, CertificateDTO>(sertificate);
        }

        public CertificateDTO GetCertificateByNumber(string number)
        {
            var sertificate = db.Certificates.Find(s => s.CertificateNumber == number).FirstOrDefault();
            return _mapper.Map<Certificate, CertificateDTO>(sertificate);
        }

        public IEnumerable<CertificateDTO> GetCertificatesByUserId(int userId)
        {
            var sertificates = db.Certificates.Find(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<Certificate>, IEnumerable<CertificateDTO>>(sertificates);
        }
    }
}
