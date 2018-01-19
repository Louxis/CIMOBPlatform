using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    /// <summary>
    /// This class represents the validation of a date for an interview, so the user cannot create an interview with 
    /// a date that is before the current date.
    /// </summary>
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
