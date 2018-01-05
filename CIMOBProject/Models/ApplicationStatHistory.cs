using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class ApplicationStatHistory
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public String ApplicationStat { get; set; }

        public DateTime DateOfUpdate { get; set; }

        public Application Application { get; set; }

    }
}
