using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class OlderThan : ValidationAttribute {

        public OlderThan() {

        }

        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext) {
            var model = (Models.Edital)validationContext.ObjectInstance;
            DateTime EndDate = Convert.ToDateTime(value);
            DateTime StartDate = Convert.ToDateTime(model.OpenDate);

            if (StartDate >= EndDate) {
                return new ValidationResult
                    ("A data de fim tem de ser maior que a data de abertura.");
            }
            else {
                return ValidationResult.Success;
            }
        }

    }
}
