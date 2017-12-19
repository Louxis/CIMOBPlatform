using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class ApplicationStat
    {
        public int Id { get; set; }

        [Required]
        [Display (Name = "Nome")]
        public String Name { get; set; }
    }
}
