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
    /// This class represents the functionalitys of the screen related to the student applications.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class ApplicationScreen : UserControl, IGetGrid {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ApplicationScreen()
        {
            InitializeComponent();
            applicationGrd.ItemsSource = _db.Applications.Select(a => new { a.Student.UserFullname, a.ApplicationStat.Name, a.BilateralProtocol1.Destination, a.CreationDate, a.FinalGrade}).ToList();
            applicationGrd.IsSynchronizedWithCurrentItem = true;
        }

        public DataGrid GetRespectiveGrid()
        {
            return applicationGrd;
        }
    }
}
