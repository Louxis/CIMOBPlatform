using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class PresentDate : ValidationAttribute {
        public PresentDate() {

        }

        public override bool IsValid(object value) {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date)) {
                return date.Year >= DateTime.Now.Year;
            }
            return false;
        }
    }
}
