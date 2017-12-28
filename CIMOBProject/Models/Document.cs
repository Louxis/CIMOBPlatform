using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Document
    {
        ///<summary>
        ///In this class we define the atributes of the documents that both Students and Employees will have to handle.
        ///The Student attribute represents the student to which it belongs.
        /// </summary>
        
        
        public int DocumentId { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "URL")]
        [Required]
        public string FileUrl { get; set; }

        [Display(Name = "Data de Carregamento")]
        [Required]
        public DateTime UploadDate { get; set; }

        public String ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
