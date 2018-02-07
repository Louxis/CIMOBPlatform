using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class CollegeSubject : INotifyPropertyChanged {
        ///<summary>
        ///This class represents the courses that students belong to.
        ///Mostly used to indicate the college of a student since the courses are specific to each college.
        /// </summary>
        
        public CollegeSubject() {

        }

        public CollegeSubject(CollegeSubject collegeSubject) {
            Id = collegeSubject.Id;
            SubjectName = collegeSubject.SubjectName;
            SubjectAlias = collegeSubject.SubjectAlias;
            CollegeId = collegeSubject.CollegeId;
        }

        public int Id { get; set; }

        private string subjectName;
        public string SubjectName {
            get { return subjectName; }
            set {
                subjectName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SubjectName"));
            }
        }

        private string subjectAlias;
        public string SubjectAlias {
            get { return subjectAlias; }
            set {
                subjectAlias = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SubjectAlias"));
            }
        }

        private int collegeId;
        public int CollegeId {
            get { return collegeId; }
            set {
                collegeId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CollegeId"));
            }
        }

        public virtual College College { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
