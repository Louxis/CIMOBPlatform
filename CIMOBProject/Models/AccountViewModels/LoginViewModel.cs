using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O email não é um email válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo password é obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar Dados?")]
        public bool RememberMe { get; set; }
    }
}
