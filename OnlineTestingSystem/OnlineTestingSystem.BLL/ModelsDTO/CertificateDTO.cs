using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.ModelsDTO
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        [StringLength(10, MinimumLength = 3)]
        public string CertificateNumber { get; set; }
        public int Score { get; set; }  
        public DateTime TestDate { get; set; }


        public int UserId { get; set; }
        public virtual UserDTO UserDTO { get; set; }
        public int TestId { get; set; }
        public virtual TestDTO TestDTO { get; set; }
    }
}
