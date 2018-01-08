using System;
using System.Collections.Generic;
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

        public string Destination { get; set; }

        public int OpenSlots { get; set; }

        public virtual CollegeSubject Subject { get; set; }
    }
}
