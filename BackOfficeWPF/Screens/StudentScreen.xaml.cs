using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the students.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class StudentScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public StudentScreen()
        {
            InitializeComponent();
            studentGrd.ItemsSource = _db.Students.Select(e => new { e.UserName, e.UserFullname, e.UserCc, e.StudentNumber, e.ALOGrade, e.CollegeSubject.SubjectName, e.Email, e.IsBanned}).ToList();
            studentGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
