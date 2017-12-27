using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public string StudentId { get; set; }

        public int ApplicationStatId { get; set; }

        public string EmployeeId { get; set; }

        public int Grade { get; set; }

        public int ECTS { get; set; }

        public virtual Student Student { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ApplicationStat ApplicationStat { get; set; }

        public virtual List<Document> Documents { get; set; }


    }
}
