﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class UserRole
    {
        public byte UserRoleID { get; set; }

        [Required]
        [StringLength(20, MinimumLength =3)]
        public string RoleName { get; set; }


        public virtual ICollection<User> Users { get; set; }

        public UserRole()
        {
            Users = new List<User>();
        }
    }
}
