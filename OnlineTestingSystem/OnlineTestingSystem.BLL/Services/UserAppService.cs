using OnlineTestingSystem.BLL.Interfaces;
using OnlineTestingSystem.DAL.Interfaces;
using OnlineTestingSystem.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestingSystem.BLL.Infrastructure;
using OnlineTestingSystem.BLL.ModelsDTO;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace OnlineTestingSystem.BLL.Services
{
    public class UserAppService : IUserAppService
    {
        IUnitOfWorkApplication Database { get; set; }

        public UserAppService(IUnitOfWorkApplication uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.UserRole.ToString());
                // создаем профиль клиента
                UserProfile clientProfile = new UserProfile { Id = user.Id, Email = userDto.Email, Name = userDto.Login };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        //public void ChangeUserRole(string userEmail, string role)
        //{
        //    var oldUser = Database.UserManager.FindByEmail(userEmail);
        //    var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
        //    var oldRoleName =Database.RoleManager.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

        //    if (oldRoleName != role)
        //    {
        //        Database.UserManager.RemoveFromRole(oldUser.Id, oldRoleName);
        //        Database.UserManager.AddToRole(oldUser.Id, role);
        //    }
        //    Database.SaveAsync();
        //}

        public void Dispose()
        {
            Database.Dispose();
        }

       
    }
}
