using System;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    /// <summary>
    /// Validation attribute to verify that a date is not older than the current year.
    /// </summary>
    public class PresentDate : ValidationAttribute
    {
        public PresentDate()
        {

        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.Year >= DateTime.Now.Year;
            }
            return false;
        }
    }
}
