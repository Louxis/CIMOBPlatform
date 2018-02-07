using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the application that the student needs to deliver in order to attempt to join a outgoing project.
    ///It has a relationship with the student to who it bellongs, with the employee who will evaluate it, with a applicationStat that defines the current state,
    ///it has a relationship with various BilateralProtocols which represent the multiple destinations a student might want to go and finally it has a list of documents that are related with it.
    ///</summary> 
    public class Application
    {
        public int ApplicationId { get; set; }

        public string StudentId { get; set; }
        [Display(Name = "Estado da candidatura")]
        public int ApplicationStatId { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "1ª Opção")]
        public int? BilateralProtocol1Id { get; set; }

        [Display(Name = "2ª Opção")]
        public int? BilateralProtocol2Id { get; set; }

        [Display(Name = "3ª Opção")]
        public int? BilateralProtocol3Id { get; set; }

        public DateTime CreationDate { get; set; }

        [Display(Name = "Média aritmética")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "Média aritemética deve ser entre 0 e 20.")]
        public double? ArithmeticMean { get; set; }

        public int? ECTS { get; set; }

        /// <summary>
        /// This will be the grade associated with the motivation letter of a student.
        /// </summary>
        [Display(Name = "Avaliação da Motivação do Estudante")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A carta de motivação tem de ter uma nota entre 0 e 20.")]
        public double? MotivationLetter { get; set; }

        /// <summary>
        /// This will be the grade associated with the interview with the student.
        /// </summary>
        [Display(Name = "Entrevista")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A entrevista deve ter uma nota de 0 e 20.")]
        public double? Interview { get; set; }

        /// <summary>
        /// The final grade calculated when an application goes by the seriation process.
        /// </summary>
        [Display(Name = "Nota final")]
        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A nota final deve ter uma nota de 0 e 20.")]
        public double? FinalGrade { get; set; }

        /// <summary>
        /// Motivation Letter content
        /// </summary>
        [Display(Name = "O que te motivou a ir de Erasmus?")]
        public string Motivations { get; set; }

        [Display(Name = "Aluno")]
        public virtual Student Student { get; set; }

        [Display(Name = "Avaliador")]
        public virtual Employee Employee { get; set; }

        [Display(Name = "Estado da candidatura")]
        public virtual ApplicationStat ApplicationStat { get; set; }

        public virtual List<Document> Documents { get; set; }

        public virtual BilateralProtocol BilateralProtocol1 { get; set; }

        public virtual BilateralProtocol BilateralProtocol2 { get; set; }

        public virtual BilateralProtocol BilateralProtocol3 { get; set; }


    }
}
