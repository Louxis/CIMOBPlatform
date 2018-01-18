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
    /// Interaction logic for NewsScreen.xaml
    /// </summary>
    public partial class NewsScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public NewsScreen()
        {
            InitializeComponent();
            newsGrd.ItemsSource = _db.News.Select(n => new { n.Title, n.TextContent, n.IsPublished, n.Employee.UserFullname}).ToList();
            newsGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
