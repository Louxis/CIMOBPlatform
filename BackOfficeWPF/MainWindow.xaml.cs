using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace BackOfficeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = _db.Users.Local;
            ListBoxEmpresas.ItemsSource = _db.Users.ToList();
            ListBoxEmpresas.IsSynchronizedWithCurrentItem = true;
        }
    }
}
