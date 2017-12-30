using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Edital : News
    {
        [Display(Name = "Data de Início de Candidaturas")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "Data de Fim de Candidaturas")]
        public DateTime CloseDate { get; set; }
    }
}
