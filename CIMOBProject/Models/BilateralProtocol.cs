using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the bilateral protocol that represents a destination that the student can select.
    ///It is made of the destination that the student can select, the number of students that can enroll on the protocol and the subject to which it belongs.
    ///Only students who have the same subject as the protocol can praticipate on it.
    ///</summary> 
    public class BilateralProtocol
    {

        public int Id { get; set; }

        public int SubjectId { get; set; }
        [Required(ErrorMessage = "O destino é obrigatório")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "O número de vagas é obrigatório.")]
        [Range(0,int.MaxValue, ErrorMessage = "O número de vagas precisa de ser positivo.")]
        public int OpenSlots { get; set; }
        public virtual CollegeSubject Subject { get; set; }
    }
}
