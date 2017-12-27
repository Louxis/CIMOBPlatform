using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class BilateralProtocol
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int CollegeId { get; set; }

        public virtual CollegeSubject Subject { get; set; }

        public virtual College College { get; set; }
    }
}
