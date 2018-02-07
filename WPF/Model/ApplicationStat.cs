using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WPF.Models
{
    ///<summary>
    ///In this class we define the atributes of the applicationStat that will represent the current state of the application.
    ///</summary> 
    public class ApplicationStat
    {
        public int Id { get; set; }

        [Required]
        [Display (Name = "Nome")]
        public string Name { get; set; }
    }
}
