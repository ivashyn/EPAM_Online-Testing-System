using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class UserDTO
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

        public UserRoleDTO UserRole { get; set; }
        public virtual ICollection<CertificateDTO> SertificatesDTO { get; set; }
        public virtual ICollection<TestSessionDTO> TestSessionsDTO { get; set; }

        public UserDTO()
        {
            SertificatesDTO = new List<CertificateDTO>();
            TestSessionsDTO = new List<TestSessionDTO>();
        }
    }
}
