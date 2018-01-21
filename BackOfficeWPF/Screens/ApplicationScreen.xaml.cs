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
    /// Interaction logic for ApplicationScreen.xaml
    /// </summary>
    public partial class ApplicationScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ApplicationScreen()
        {
            InitializeComponent();
            applicationGrd.ItemsSource = _db.Applications.Select(a => new { a.ApplicationId, a.Student.UserFullname, a.ApplicationStat.Name, a.BilateralProtocol1.Destination, a.CreationDate, a.FinalGrade}).ToList();
            applicationGrd.IsSynchronizedWithCurrentItem = true;
        }

        public void Refresh() {
            applicationGrd.ItemsSource = _db.Applications.Select(a => new { a.ApplicationId, a.Student.UserFullname, a.ApplicationStat.Name, a.BilateralProtocol1.Destination, a.CreationDate, a.FinalGrade }).ToList();
        }
    }
}
