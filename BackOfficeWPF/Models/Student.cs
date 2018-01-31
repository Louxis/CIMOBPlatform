using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the student that extends from the ApplicationUser.
    ///The most important atributes of this class are the Documents and the CollegeSubject since they represent the relationship that
    ///this class within the data base.
    ///</summary>  
    public class Student : ApplicationUser {

        public Student() {

        }

        public Student (Student user) : base(user) {
            StudentNumber = user.StudentNumber;
            ALOGrade = user.ALOGrade;
            CollegeSubjectId = user.CollegeSubjectId;
            CollegeId = user.CollegeId;
        }

        private string studentNumber;
        public string StudentNumber {
            get { return studentNumber; }
            set {
                studentNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StudentNumber"));
            }
        }

        private int aloGrade;
        public int ALOGrade {
            get { return aloGrade; }
            set {
                aloGrade = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ALOGrade"));
            }
        }

        [Display(Name = "Curso")]
        public int CollegeSubjectId { get; set; }

        public int? CollegeId { get; set; }

        public virtual CollegeSubject CollegeSubject { get; set; }

        public virtual List<Application> Applications { get; set; }

    }
}
