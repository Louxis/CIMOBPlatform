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
    /// Interaction logic for CollegeScreen.xaml
    /// </summary>
    public partial class CollegeScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public CollegeScreen()
        {
            InitializeComponent();
            collegeGrd.ItemsSource = _db.Colleges.Select(c => new { c.CollegeName, c.CollegeAlias, }).ToList();
            collegeGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
