using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}