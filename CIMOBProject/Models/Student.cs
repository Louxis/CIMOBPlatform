using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Student {

        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string StudentNumber { get; set; }
        public int ALOGrade { get; set; }
        [Required]
        public int CollegeID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<Document> Documents { get; set; }

        public virtual College College { get; set; }

    }
}
