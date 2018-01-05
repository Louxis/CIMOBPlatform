using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the student that extends from the ApplicationUser.
    ///The most important atributes of this class are the Documents and the CollegeSubject since they represent the relationship that
    ///this class within the data base.
    ///</summary>  
    public class Student : ApplicationUser {

        [Required(ErrorMessage = "Número de estudante é obrigatório.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "O número de estudante não é válido.")]
        [Display(Name = "Número de Estudante")]
        public string StudentNumber { get; set; }

        [Display(Name = "Nota do teste de Línguas")]
        public int ALOGrade { get; set; }

        [Display(Name = "Curso")]
        public int CollegeSubjectId { get; set; }

        public virtual List<Document> Documents { get; set; }

        public virtual CollegeSubject CollegeSubject { get; set; }

        public virtual List<Application> Applications { get; set; }

    }
}
