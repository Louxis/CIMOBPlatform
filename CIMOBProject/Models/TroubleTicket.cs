using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    /// <summary>
    /// This class represents a trouble ticket that a student or employee can send in case of problem.
    /// It has a title, a description of the problem, a creation date, a state that determines if it is solved,
    /// the user that sends the ticket and the student number, in case the trouble ticket sender is an employee.
    /// It also has a list that represents all the dialog between student-employee for that problem.
    /// </summary>
    public class TroubleTicket
    {
        public int TroubleTicketId { get; set; }

        [Required]
        [Display(Name = "Título")]
        public String Title { get; set; }

        [Required]
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
