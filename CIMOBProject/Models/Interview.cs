using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///This class represents an interview that is done with a student about the mobility.
    ///It has the application to which the interview is done, it's date and time and the employee that created it.
    ///</summary>

    public class Interview
    {
        public int InterviewId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee {get; set;}
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        [ValidateDay]
        [DataType(DataType.Date)]
        [Display(Name = "Data da entrevista")]
        public DateTime InterviewDate { get; set; }
    }
}
