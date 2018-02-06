using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    /// <summary>
    /// Validation to check if the close date (input field) is after the open date (another input field).
    /// </summary>
    public class OlderThan : ValidationAttribute
    {

        public OlderThan()
        {

        }

        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            var model = (Edital)validationContext.ObjectInstance;
            DateTime EndDate = Convert.ToDateTime(value);
            DateTime StartDate = Convert.ToDateTime(model.OpenDate);

            if (StartDate >= EndDate)
            {
                return new ValidationResult
                    ("A data de fim tem de ser maior que a data de abertura.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

    }
}
