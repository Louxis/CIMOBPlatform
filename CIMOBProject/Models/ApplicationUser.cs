using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nome completo")]
        public string UserFullname { get; set; }
        [Required]
        public String PostalCode { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public String UserAddress { get; set; }
        [Required]
        public int UserCc { get; set; }

    }
}
