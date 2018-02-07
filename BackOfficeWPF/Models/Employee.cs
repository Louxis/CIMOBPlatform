using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Employee : ApplicationUser
    {
        public Employee() : base() {

        }

        public Employee(Employee user) : base(user) {
            EmployeeNumber = user.EmployeeNumber;
            UserName = user.UserName;
        }

        ///<summary>
        ///In this class we define the atributes of the employee that extends from the ApplicationUser.
        ///Unlike the student, that also extends from ApplicationUser, this one does not contain as many attributes since the employees
        ///do not require any relevante information besides that one already included in the ApplicationUser
        /// </summary>
        private string emplyoeeNUmber;
        public string EmployeeNumber {
            get { return emplyoeeNUmber; }
            set {
                emplyoeeNUmber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EmployeeNumber"));
            }
        }        
    }
}
