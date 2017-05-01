using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(300)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }


        public byte UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<Sertificate> Sertificates { get; set; }
        public virtual ICollection<TestSession> TestSessions { get; set; }

        public User()
        {
            Sertificates = new List<Sertificate>();
            TestSessions = new List<TestSession>();
        }
        
    }
}
