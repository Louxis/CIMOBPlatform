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
        [Display(Name = "Código Postal")]
        public String PostalCode { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Morada")]
        public String UserAddress { get; set; }

        [Required]
        [Display(Name = "Número de Cartão de Cidadão")]
        public int UserCc { get; set; }

        [Display(Name = "Número de Telefone")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    }
}
