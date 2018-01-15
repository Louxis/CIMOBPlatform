using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Models
{
    ///<summary>
    ///In this class we define the atributes of the quizz that will be delivered to students when they finish theyer outgoing process.
    ///It is composed of a year, a semester to which it belongs, a QuizzUrl to take the student to the quizz and finally a bool that represents if it as been published or not. 
    ///</summary> 
    public class Quizz
    {
        public int Id { get; set; }
        [Range(1900,int.MaxValue, ErrorMessage = "Ano inválido")]
        [Display(Name = "Ano")]
        public int Year { get; set; }
        [Range(1,2, ErrorMessage = "Só existem dois semestres por ano")]
        [Display(Name = "Semestre")]
        public int Semester { get; set; }
        [Required(ErrorMessage = "O link para o questionário é obrigatório")]
        [Display(Name = "Link de Questionário")]
        public string QuizzUrl { get; set; }
        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }
    }
}
