using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class College {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CollegeName { get; set; }
        public virtual List<Student> Students { get; set; }
        public virtual List<CollegeSubject> Subjects { get; set; }
    }
}
