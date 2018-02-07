using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    ///<summary>
    ///This class represents the courses that students belong to.
    ///Mostly used to indicate the college of a student since the courses are specific to each college.
    /// </summary>
    public class CollegeSubject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Curso")]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Sigla do curso")]
        public string SubjectAlias { get; set; }

        [Required]
        public int CollegeId { get; set; }

        public virtual College College { get; set; }
    }
}
