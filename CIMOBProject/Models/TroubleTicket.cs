using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    public class TroubleTicket
    {
        public int TroubleTicketId { get; set; }

        [Display(Name = "Título")]
        public String Title { get; set; }

        [Display(Name = "Descrição")]
        public String Description { get; set; }

        [Display(Name = "Data de Envio")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Resolvido")]
        public bool Solved { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Respostas")]
        public virtual List<TroubleTicketAnswer> Answers { get; set; }
        
        [Display(Name = "Número de Estudante")]
        public String StudentNumber { get; set; }
    }
}
