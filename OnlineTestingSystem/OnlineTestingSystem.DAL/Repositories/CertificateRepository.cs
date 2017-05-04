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
    public class CertificateRepository : IRepository<Certificate>
    {
        TestContext db;

        public CertificateRepository(TestContext context)
        {
            db = context;
        }

        public void Create(Certificate item)
        {
            db.Certificates.Add(item);
        }

        public void Delete(int id)
        {
            Certificate sertificate = db.Certificates.Find(id);
            if (sertificate != null)
                db.Certificates.Remove(sertificate);
        }

        public IEnumerable<Certificate> Find(Func<Certificate, bool> predicate)
        {
            return db.Certificates.Where(predicate).ToList();
        }

        public Certificate Get(int id)
        {
            return db.Certificates.Find(id);
        }

        public IEnumerable<Certificate> GetAll()
        {
            return db.Certificates;
        }

        public void Update(Certificate item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
