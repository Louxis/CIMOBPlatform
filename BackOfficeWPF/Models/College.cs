using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{

    public class College : INotifyPropertyChanged {
        ///<summary>
        ///This class represents the colleges students belong to.
        ///It posseses a list of Students and a list of course represent a relationship of one to many in the DB.
        /// </summary>
         
        public College() {

        }

        public College(College college) {
            Id = college.Id;
            CollegeName = college.CollegeName;
            CollegeAlias = college.CollegeAlias;
        }

        public int Id { get; set; }

        private string collegeName;
        public string CollegeName {
            get { return collegeName; }
            set {
                collegeName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CollegeName"));
            }
        }

        private string collegeAlias;
        public string CollegeAlias {
            get { return collegeAlias; }
            set {
                collegeAlias = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CollegeAlias"));
            }
        }

        public virtual List<Student> Students { get; set; }
        public virtual List<CollegeSubject> Subjects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
