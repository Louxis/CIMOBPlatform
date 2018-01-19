using CIMOBProject.Models;
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
using System.Windows.Shapes;

namespace BackOfficeWPF {
    /// <summary>
    /// Interaction logic for BilateralProtocolScreen.xaml
    /// </summary>
    public partial class BilateralProtocolScreen : UserControl {
        ApplicationDbContext _db = new ApplicationDbContext();
        public BilateralProtocol currentProtocol;
        public BilateralProtocolScreen()
        {
            InitializeComponent();
            bilateralGrd.ItemsSource = _db.BilateralProtocols.Select(e => new { e.Destination, e.Subject.SubjectName, e.OpenSlots }).ToList();
            bilateralGrd.IsSynchronizedWithCurrentItem = true;
            currentProtocol = _db.BilateralProtocols.First();
        }
    }
}
