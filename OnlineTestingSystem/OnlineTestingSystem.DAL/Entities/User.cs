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
        [StringLength(35, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public byte UserRoleId { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<TestSession> TestSessions { get; set; }

        public User()
        {
            Certificates = new List<Certificate>();
            TestSessions = new List<TestSession>();
        }
        
    }
}
