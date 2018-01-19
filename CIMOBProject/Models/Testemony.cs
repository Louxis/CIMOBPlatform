using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    /// <summary>
    /// This class represents a testemony publication that a student can do about his mobility.
    /// It has title, content, and creation date. An employee validates the text and if the text is good, it will be published.
    /// </summary>
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
