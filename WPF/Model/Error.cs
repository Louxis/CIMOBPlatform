using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WPF.Models
{
    ///<summary>
    ///We use this class in order to display the errors that might ocurre with the system.
    ///</summary> 
    public class Error
    {
        public int Id { get; set; }

        [Required]
        public string ErrorCode { get; set; }

        [Required]
        public int ErrorDescription { get; set; }
    }
}
