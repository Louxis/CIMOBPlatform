using System;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    /// <summary>
    /// This class represents a testemony publication that a student can do about his mobility.
    /// It has title, content, and creation date. An employee validates the text and if the text is good, it will be published.
    /// </summary>
    public class Testemony
    {
        public int TestemonyId { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Título")]
        public String Title { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Conteúdo")]
        public String Content { get; set; }

        public String StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [Display(Name = "Data Criação")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Publicada")]
        public bool Valid { get; set; }
    }
}
