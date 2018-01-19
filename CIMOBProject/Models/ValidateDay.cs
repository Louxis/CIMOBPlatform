using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class ValidateDay : ValidationAttribute {

        public ValidateDay() {

        }

        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext) {
            DateTime Date = Convert.ToDateTime(value);

            if (Date < DateTime.Now) {
                return new ValidationResult
                    ("Não pode marcar entrevistas antes da data de hoje.");
            }
            else {
                return ValidationResult.Success;
            }
        }

    }
}
