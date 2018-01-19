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

namespace BackOfficeWPF.Screens {
    /// <summary>
    /// Interaction logic for SubjectScreen.xaml
    /// </summary>
    public partial class SubjectScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public SubjectScreen()
        {
            InitializeComponent();
            subjectGrd.ItemsSource = _db.CollegeSubjects.Select(c => new { c.SubjectName, c.SubjectAlias, c.College.CollegeName}).ToList();
            subjectGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
