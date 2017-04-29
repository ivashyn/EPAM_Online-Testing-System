using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class UserRole
    {
        public int UserRoleID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }


        public virtual ICollection<User> Users { get; set; }

        public UserRole()
        {
            Users = new List<User>();
        }
    }
}
