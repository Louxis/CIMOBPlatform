using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Testemony
    {
        public int TestemonyId { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Valid { get; set; }
    }
}
