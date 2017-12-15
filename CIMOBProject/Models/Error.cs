using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Error
    {
        public int Id { get; set; }

        [Required]
        public string ErrorCode { get; set; }

        [Required]
        public int ErrorDescription { get; set; }
    }
}
