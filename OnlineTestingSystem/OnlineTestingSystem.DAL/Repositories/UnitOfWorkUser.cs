﻿using OnlineTestingSystem.DAL.EntityFramework;
using OnlineTestingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestingSystem.DAL.Entities;

namespace OnlineTestingSystem.DAL.Repositories
{
    public class UnitOfWorkUser : IUnitOfWorkUser
    {
        TestContext db = new TestContext();
        private UserRepository userRepository;
        private UserRoleRepository userRoleRepository;

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<UserRole> UserRoles {
            get
            {
                if (userRoleRepository == null)
                    userRoleRepository = new UserRoleRepository(db);
                return userRoleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
