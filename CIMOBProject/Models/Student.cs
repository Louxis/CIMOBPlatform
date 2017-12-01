using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Student : ApplicationUser {

        [Required]
        [StringLength(12)]
        public string StudentNumber { get; set; }
        public int ALOGrade { get; set; }
        [Required]
        public int CollegeId { get; set; }
        public int CollegeSubjectId { get; set; }
        public virtual List<Document> Documents { get; set; }
        public virtual College College { get; set; }
        public virtual CollegeSubject CollegeSubject { get; set; }

    }
}
