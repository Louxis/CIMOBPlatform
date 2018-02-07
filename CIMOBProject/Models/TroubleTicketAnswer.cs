using System;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    /// <summary>
    /// This class represents a trouble ticket answer that is used to create a dialog between the student and the employee
    /// about the trouble ticket's problem.
    /// It has the content of the message, the sender and the creation date.
    /// </summary>
    public class TroubleTicketAnswer
    {
        public int TroubleTicketAnswerId { get; set; }

        [Required]
        [Display(Name = "Resposta")]
        public String Content { get; set; }

        public int TroubleTicketId { get; set; }
        public virtual TroubleTicket TroubleTicket { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Data de Envio")]
        public DateTime CreationDate { get; set; }

        public int? DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
