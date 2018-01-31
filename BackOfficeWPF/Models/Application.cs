using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the application that the student needs to deliver in order to attempt to join a outgoing project.
    ///It has a relationship with the student to who it bellongs, with the employee who will evaluate it, with a applicationStat that defines the current state,
    ///it has a relationship with various BilateralProtocols which represent the multiple destinations a student might want to go and finally it has a list of documents that are related with it.
    ///</summary> 
    public class Application : INotifyPropertyChanged
    {
        public Application() {

        }

        public Application(Application application) {
            ApplicationId = application.ApplicationId;
            ApplicationStatId = application.ApplicationStatId;
        }

        public int ApplicationId { get; set; }
       
        public string StudentId { get; set; }

        private int applicationStatId;
        public int ApplicationStatId {
            get { return applicationStatId; }
            set {
                applicationStatId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ApplicationStatId"));
                OnPropertyChanged(new PropertyChangedEventArgs("ApplicationStat"));
            }
        }

        public string EmployeeId { get; set; }

        [Display(Name = "1ª Opção")]
        public int? BilateralProtocol1Id { get; set; }

        [Display(Name = "2ª Opção")]
        public int? BilateralProtocol2Id { get; set; }

        [Display(Name = "3ª Opção")]
        public int? BilateralProtocol3Id { get; set; }

        public DateTime CreationDate { get; set; }

        [Display(Name = "Média aritmética")]
        [Range(minimum:0.0, maximum:20.0, ErrorMessage ="Média aritemética deve ser entre 0 e 20.")]
        public double? ArithmeticMean { get; set; }

        public int? ECTS { get; set; }

        [Display(Name = "Avaliação da Motivação do Estudante")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A carta de motivação tem de ter uma nota entre 0 e 20.")]
        public double? MotivationLetter { get; set; }

        [Display(Name = "Entrevista")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A entrevista deve ter uma nota de 0 e 20.")]
        public double? Enterview { get; set; }

        [Display(Name = "Nota final")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A nota final deve ter uma nota de 0 e 20.")]
        public double? FinalGrade { get; set; }

        [Display(Name = "O que te motivou a ir de Erasmus?")]
        public string Motivations { get; set; }

        public virtual Student Student { get; set; }

        [Display(Name = "Avaliador")]
        public virtual Employee Employee { get; set; }

        [Display(Name = "Estado da candidatura")]
        public virtual ApplicationStat ApplicationStat { get; set; }

        public virtual List<Document> Documents { get; set; }

        public virtual BilateralProtocol BilateralProtocol1 { get; set; }

        public virtual BilateralProtocol BilateralProtocol2 { get; set; }

        public virtual BilateralProtocol BilateralProtocol3 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
