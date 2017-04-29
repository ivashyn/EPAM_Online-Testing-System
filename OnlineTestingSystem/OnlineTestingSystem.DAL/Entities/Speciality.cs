using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.DAL.Entities
{
    public class Speciality
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }


        public virtual ICollection<Test> Tests { get; set; }

        public Speciality()
        {
            Tests = new List<Test>();
        }

    }
}
