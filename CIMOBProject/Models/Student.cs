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
        [Display(Name = "Número de Estudante")]
        public string StudentNumber { get; set; }


        [Display(Name = "Nota do teste de Línguas")]
        public int ALOGrade { get; set; }

        [Required]
        [Display(Name = "Escola")]
        public int CollegeId { get; set; }

        [Display(Name = "Curso")]
        public int CollegeSubjectId { get; set; }

        public virtual List<Document> Documents { get; set; }
        [Display(Name = "Escola")]
        public virtual College College { get; set; }
        public virtual CollegeSubject CollegeSubject { get; set; }

    }
}
