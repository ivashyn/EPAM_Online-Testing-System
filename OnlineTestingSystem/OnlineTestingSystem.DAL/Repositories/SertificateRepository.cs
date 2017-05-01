using OnlineTestingSystem.DAL.Entities;
using OnlineTestingSystem.DAL.EntityFramework;
using OnlineTestingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Repositories
{
    public class SertificateRepository : IRepository<Sertificate>
    {
        TestContext db;

        public SertificateRepository(TestContext context)
        {
            db = context;
        }

        public void Create(Sertificate item)
        {
            db.Sertificates.Add(item);
        }

        public void Delete(int id)
        {
            Sertificate sertificate = db.Sertificates.Find(id);
            if (sertificate != null)
                db.Sertificates.Remove(sertificate);
        }

        public IEnumerable<Sertificate> Find(Func<Sertificate, bool> predicate)
        {
            return db.Sertificates.Where(predicate).ToList();
        }

        public Sertificate Get(int id)
        {
            return db.Sertificates.Find(id);
        }

        public IEnumerable<Sertificate> GetAll()
        {
            return db.Sertificates;
        }

        public void Update(Sertificate item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
