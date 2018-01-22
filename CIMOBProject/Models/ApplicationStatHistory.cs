using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the application history that will track the changes made to each application over time.
    ///This will be used to present some feedback to the student who as applied to the outgoing project.
    ///</summary> 
    public class ApplicationStatHistory
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        [Display(Name = "Estado da candidatura")]
        public String ApplicationStat { get; set; }

        [Display(Name = "Data da modificação")]
        public DateTime DateOfUpdate { get; set; }

        public virtual Application Application { get; set; }

    }
}
