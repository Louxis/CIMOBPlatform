using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Employee : ApplicationUser
    {
        ///<summary>
        ///In this class we define the atributes of the employee that extends from the ApplicationUser.
        ///Unlike the student, that also extends from ApplicationUser, this one does not contain as many attributes since the employees
        ///do not require any relevante information besides that one already included in the ApplicationUser
        /// </summary>
        /// 
        [Display(Name = "Número de Funcionário")]
        public int EmployeeNumber { get; set; }
    }
}
