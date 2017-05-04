using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
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
