using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{

    public class College {
        ///<summary>
        ///This class represents the colleges students belong to.
        ///It posseses a list of Students and a list of course represent a relationship of one to many in the DB.
        /// </summary>
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Escola")]
        public string CollegeName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Sigla")]
        public string CollegeAlias { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<CollegeSubject> Subjects { get; set; }
    }
}
