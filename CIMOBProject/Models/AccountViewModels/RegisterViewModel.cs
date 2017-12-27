using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(256)]
        [Display(Name = "Nome")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é um email válido!")]
        [StringLength(256)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória.")]
        [StringLength(16, ErrorMessage = "A {0} password tem de ter no minimo {2} e no máximo {1} caracteres.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "As passwords não são identicas.")]
        public string ConfirmPassword { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "O número de telemóvel é obrigatório.")]
        [Display(Name = "Número de Telemóvel")]
        [RegularExpression(@"^[2356789]{1}[0-9]{8}$", ErrorMessage = "Não é um número válido.")]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "A morada é obrigatória.")]
        [Display(Name = "Morada")]
        [StringLength(450, MinimumLength = 5, ErrorMessage = "A morada precisa de conter pelo menos 5 digitos.")]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "O código postal é obrigatório.")]
        [Display(Name = "Código Postal")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[0-9]{4}[ -]?[0-9]{3}$", ErrorMessage = "O código postal não é válido")]
        public String PostalCode { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RegisterAge(17, ErrorMessage = "A idade minima é 17 anos.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O CC é obrigatório.")]
        [Display(Name = "CC")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "O CC precisa de conter 8 digitos")]
        public string UserCc { get; set; }

        [Required(ErrorMessage = "Número de estudante é obrigatório.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "O número de estudante não é válido.")]
        [Display(Name = "Número de Estudante")]
        public string StudentNumber { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Não foi selecionado um curso.")]
        public int CollegeSubjectId { get; set; }
    }
}
