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
using System.Security.Cryptography;

namespace OnlineTestingSystem.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWorkUser db;
        IMapper _mapper;

        public UserService(IUnitOfWorkUser uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                .ForMember(udto => udto.UserRole, dal => dal.MapFrom(u => (UserRoleDTO)u.UserRoleId))
                .ForMember(udto => udto.SertificatesDTO, dal => dal.MapFrom(u => u.Certificates))
                .ForMember(udto => udto.TestSessionsDTO, dal => dal.MapFrom(u => u.TestSessions));
                cfg.CreateMap<UserDTO, User>()
                .ForMember(u => u.UserRoleId,
                        dal => dal.MapFrom(udto => (byte)((UserRoleDTO)Enum.Parse(typeof(UserRoleDTO), udto.UserRole.ToString()))));
                cfg.CreateMap<Certificate, CertificateDTO>();
                cfg.CreateMap<CertificateDTO, Certificate>();
                cfg.CreateMap<TestSession, TestSessionDTO>();
                cfg.CreateMap<TestSessionDTO, TestSession>();
            });
            _mapper = config.CreateMapper();
        }

        public void CreateUser(UserDTO user)
        {
            var userFromDb = db.Users.Find(u => u.Email == user.Email).FirstOrDefault();
            if (userFromDb != null)
                throw new ValidationException("Sorry, but the user with the same Email is already exsist", "");
            var userToAdd = _mapper.Map<UserDTO, User>(user);
            userToAdd.Password = CalculateMd5Hash(userToAdd.Password);
            db.Users.Create(userToAdd);
            db.Save();
        }

        public void DeleteUser(int id)
        {
            var userToDelete = GetUserById(id);
            if (userToDelete == null)
                throw new ValidationException("Sorry, but the user doesn't exsist", "");
            db.Users.Delete(id);
            db.Save();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = db.Users.GetAll().ToList();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserByLogin(string login)
        {
            var user = db.Users.Find(u => u.Login == login).FirstOrDefault();
            return _mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = db.Users.Find(u => u.Email == email).FirstOrDefault();
            return _mapper.Map<User,UserDTO>(user);
        }

        public UserDTO GetUserById(int id)
        {
            var user = db.Users.Get(id);
            return _mapper.Map<User, UserDTO>(user);
        }

        public IEnumerable<CertificateDTO> GetUsersCertificates(int userId) //...
        {
            var user = GetUserById(userId);
            if (user == null)
                throw new ValidationException("Sorry, but the user doesn't exsist","");
            var sertificates = user.SertificatesDTO;
            return sertificates;
        }

        public void UpdateUser(UserDTO user)
        {
            var userDAL = _mapper.Map<UserDTO, User>(user);
            db.Users.Update(userDAL);
            db.Save();
        }

        private string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();

            foreach (var c in hash)
            {
                sb.Append(c.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
