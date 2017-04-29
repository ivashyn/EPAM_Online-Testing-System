using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class TestSession
    {
        public int Id { get; set; }

        public bool IsPassed { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime TimeStart { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime TimeFinish { get; set; }

        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}
