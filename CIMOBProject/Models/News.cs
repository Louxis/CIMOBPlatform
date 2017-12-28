using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class News
    {
        public int Id { get; set; }

        
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(500)]
        public String Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }

        public bool IsPublished { get; set; }

        public int DocumentId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Document Document { get; set; }
    }
}
