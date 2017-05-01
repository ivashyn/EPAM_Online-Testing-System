using OnlineTestingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.EntityFramework
{
    public class TestContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestSession> TestSessions { get; set; }
        public DbSet<Sertificate> Sertificates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        //public DbSet<Speciality> Specialities { get; set; }

        public TestContext() : base("TestDB")
        {

        }

    }
}
