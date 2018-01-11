using CIMOBProject.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the edital which extends news.
    ///Unlike the news this class also has a open date and a close date that represent the start and finish of the current period to enroll on the outgoing process.
    ///</summary> 
    public class Edital : News
    {
        [DataType(DataType.Date)]
        [Display(Name = "Data de Início de Candidaturas")]
        [Required]
        [PresentDate(ErrorMessage = "Não pode criar editais no passado..")]
        public DateTime OpenDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Fim de Candidaturas")]
        [OlderThan]
        [Required]
        [PresentDate(ErrorMessage = "Não pode criar editais no passado..")]
        public DateTime CloseDate { get; set; }
    }
}
