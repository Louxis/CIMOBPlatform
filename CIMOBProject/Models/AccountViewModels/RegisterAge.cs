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
