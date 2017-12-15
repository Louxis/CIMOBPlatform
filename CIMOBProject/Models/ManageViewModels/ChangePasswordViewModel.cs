using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "A password atual é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "A nova password é obrigatória.")]
        [StringLength(100, ErrorMessage = " {0} deverá ter pelo menos {2} e no maximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar a nova password")]
        [Compare("NewPassword", ErrorMessage = "A nova password e a sua confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
