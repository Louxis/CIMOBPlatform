using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
       
        public string StudentId { get; set; }
        [Display(Name = "Estado da candidatura")]
        public int ApplicationStatId { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "Destino")]
        public int? BilateralProtocol1Id { get; set; }

        public int? BilateralProtocol2Id { get; set; }

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

        public virtual Employee Employee { get; set; }

        public virtual ApplicationStat ApplicationStat { get; set; }

        public virtual List<Document> Documents { get; set; }

        public virtual BilateralProtocol BilateralProtocol1 { get; set; }

        public virtual BilateralProtocol BilateralProtocol2 { get; set; }

        public virtual BilateralProtocol BilateralProtocol3 { get; set; }


    }
}
