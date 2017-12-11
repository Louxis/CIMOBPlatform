using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using CIMOBProject.Data;

namespace CIMOBProject.Models {
    ///<summary>
    ///This class is used to fill the data base with the information we requier.
    ///Mostly used to insert data that is requiered since the start of the program like the roles, colleges and college subjects.
    /// </summary>
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.SingleOrDefault(r => r.Name == "Student") == null)
            {
                
                context.Roles.Add(new IdentityRole { Name = "Student", NormalizedName = "Student" });
                context.SaveChanges();
            }

            if (context.Roles.SingleOrDefault(r => r.Name == "Employee") == null)
            {
                context.Roles.Add(new IdentityRole { Name = "Employee", NormalizedName = "Employee" });
                context.SaveChanges();
            }
            /*
            if(context.Colleges.First() == null) {
                context.Colleges.Add(new College { CollegeAlias = "ESTS", CollegeName = "Escola Superior de Tecnologia de Setúbal"});
                context.Colleges.Add(new College { CollegeAlias = "ESCE", CollegeName = "Escola Superior de Ciências Empresariais" });
                context.Colleges.Add(new College { CollegeAlias = "ESE", CollegeName = "Escola Superior de Educação" });
                context.Colleges.Add(new College { CollegeAlias = "ESTB", CollegeName = "Escola Superior de Tecnologia do Barreiro" });
                context.SaveChanges();
            }

            if (context.CollegeSubjects.First() == null) {
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EI", SubjectName = "Engenharia Informática", College = new College { Id = 1 } });
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EM", SubjectName = "Engenharia Mecânica", College = new College { Id = 1 } });
                context.SaveChanges();
            }*/

            if (context.Helps.First() == null)
            {
                context.Helps.Add(new Help {HelpDescription = "O nome deve ser constituido por vários caracteres que não incluam digitos nem caracteres especiais (*,+,_,etc.). Exe: Fernando Pessoa" });
                context.Helps.Add(new Help { HelpDescription = "O email seguir a estrutura valida dos emails. Exe: nomeExemplo@dominio.com." });
                context.Helps.Add(new Help { HelpDescription = "A password tem de ter um minimo de 6 caracteres. Exe: 123456." });
                context.Helps.Add(new Help { HelpDescription = "A password tem de ser identica à que foi introduzida anteriormente. Seguindo o exemplo anterior: 123456." });
                context.Helps.Add(new Help { HelpDescription = "O numero de telemóvel tem de ter 9 digitos. Exe: 960000000" });
                context.Helps.Add(new Help { HelpDescription = "Deve conter o nome da rua, o edificio e o andar. Exe: Avenida Dom Afonso Henriques nº 1" });
                context.Helps.Add(new Help { HelpDescription = "Deve seguir a estrutura dos códigos postas. Exe: 2000-100" });
                context.Helps.Add(new Help { HelpDescription = "A data de nascimento deve seguir a estrutura de mês, dia, ano. Exe: 1/13/1994" });
                context.Helps.Add(new Help { HelpDescription = "O CC deve ser constituido por 8 digitos." });
                context.Helps.Add(new Help { HelpDescription = "O numero de estudante deve ser constituido por 9 digitos." });
                context.Helps.Add(new Help { HelpDescription = "Deve selecionar um curso da lista. Exe: EI" });


                context.SaveChanges();
            }
            
        }
    }




    
}
