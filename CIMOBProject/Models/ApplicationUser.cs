﻿using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    /// <summary>
    /// This class will save the personal info of a user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nome completo")]
        public string UserFullname { get; set; }

        [Required(ErrorMessage = "O código postal é obrigatório.")]
        [Display(Name = "Código Postal")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[0-9]{4}[ -]?[0-9]{3}$", ErrorMessage = "O código postal não é válido")]
        public String PostalCode { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A morada é obrigatória.")]
        [Display(Name = "Morada")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "A morada precisa de conter pelo menos 5 digitos e no máximo 200.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public String UserAddress { get; set; }

        [Required(ErrorMessage = "O CC é obrigatório.")]
        [Display(Name = "Número de Cartão de Cidadão")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "O CC precisa de conter 8 digitos")]
        public string UserCc { get; set; }

        [Required(ErrorMessage = "O número de telemóvel é obrigatório.")]
        [Display(Name = "Número de Telemóvel")]
        [RegularExpression(@"^[2356789]{1}[0-9]{8}$", ErrorMessage = "Não é um número válido.")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Display(Name = "Banido")]
        public bool IsBanned { get; set; }

        public bool IsNotified { get; set; }

    }
}
