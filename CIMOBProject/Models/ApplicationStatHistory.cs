using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class ApplicationStatHistory
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        [Display(Name = "Estado da candidatura")]
        public String ApplicationStat { get; set; }
        [Display(Name = "Data da modificação")]
        public DateTime DateOfUpdate { get; set; }

        public Application Application { get; set; }

    }
}
