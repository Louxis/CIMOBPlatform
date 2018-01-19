using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Interview
    {
        public int InterviewId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee {get; set;}
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        [ValidateDay]
        public DateTime InterviewDate { get; set; }
    }
}
