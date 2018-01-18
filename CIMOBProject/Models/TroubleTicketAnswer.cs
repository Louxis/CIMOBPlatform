using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class TroubleTicketAnswer
    {
        public int TroubleTicketAnswerId { get; set;}
        public String Content { get; set; }
        public int TroubleTicketId { get; set; }
        public virtual TroubleTicket TroubleTicket { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
