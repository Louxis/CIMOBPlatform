using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the applicationStat that will represent the current state of the application.
    ///</summary> 
    public class ApplicationStat : INotifyPropertyChanged {


        public int Id { get; set; }

        private string name;
        public string Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
