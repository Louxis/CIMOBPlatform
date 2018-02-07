using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the bilateral protocol that represents a destination that the student can select.
    ///It is made of the destination that the student can select, the number of students that can enroll on the protocol and the subject to which it belongs.
    ///Only students who have the same subject as the protocol can praticipate on it.
    ///</summary> 
    public class BilateralProtocol : INotifyPropertyChanged {

        public BilateralProtocol() {

        }

        public BilateralProtocol(BilateralProtocol bilateral) {
            SubjectId = bilateral.SubjectId;
            Id = bilateral.Id;
            Destination = bilateral.Destination;
            OpenSlots = bilateral.OpenSlots;
        }

        public int Id { get; set; }

        private int subjectId;
        public int SubjectId {
            get { return subjectId; }
            set {
                subjectId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SubjectId"));
            }
        }

        private string destination;
        public string Destination {
            get { return destination; }
            set {
                destination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Destination"));
            }
        }

        private int openSlots;
        public int OpenSlots {
            get { return openSlots; }
            set {
                openSlots = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OpenSlots"));
            }
        }

        public virtual CollegeSubject Subject {get; set;}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
