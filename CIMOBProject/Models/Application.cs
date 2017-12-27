﻿using System;
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

        public int ApplicationStatId { get; set; }

        public string EmployeeId { get; set; }

        //public int Grade { get; set; }

        [Range(minimum:0.0, maximum:20.0, ErrorMessage ="Média aritemética deve ser entre 0 e 20.")]
        public double ArithmeticMean { get; set; }

        public int ECTS { get; set; }

        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A carta de motivação tem de ter uma nota entre 0 e 20.")]
        public double MotivationLetter { get; set; }

        [Range(minimum: 0.0, maximum: 20.0, ErrorMessage = "A entrevista deve ter uma nota de 0 e 20.")]
        public double Enterview { get; set; }

        public virtual Student Student { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ApplicationStat ApplicationStat { get; set; }

        public virtual List<Document> Documents { get; set; }


    }
}