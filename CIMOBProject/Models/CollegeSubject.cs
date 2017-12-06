using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class CollegeSubject {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(10)]
        public string SubjectAlias { get; set; }

        [Required]
        public int CollegeId { get; set; }

        public virtual College College { get; set; }
    }
}
