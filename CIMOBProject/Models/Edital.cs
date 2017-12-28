using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Edital : News
    {
        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }
    }
}
