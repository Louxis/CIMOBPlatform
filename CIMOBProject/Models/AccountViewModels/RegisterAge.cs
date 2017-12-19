using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models.AccountViewModels
{
    
    public class RegisterAge : ValidationAttribute 
    {
        private int minimumAge;

        /// <summary>
        /// This validation attribute will be used to validate the minimum age requirement for the student. If the input birth date
        /// calculated age is lesser than the specified by the argument minimumAge, an error will be triggered, invalidating the model
        /// using the annotation.
        /// </summary>
        /// <param name="minimumAge"></param>
        public RegisterAge(int minimumAge) {
            this.minimumAge = minimumAge;
        }

        public override bool IsValid(object value) {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date)) {
                return date.AddYears(minimumAge) < DateTime.Now;
            }
            return false;
        }
    }
}
