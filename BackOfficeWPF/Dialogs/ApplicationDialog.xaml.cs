using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CIMOBProject.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BackOfficeWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for ApplicationDialog.xaml
    /// </summary>
    public partial class ApplicationDialog : Window
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private bool mayClose;
        public CIMOBProject.Models.Application Application { get; set; }
        public ApplicationDialog(CIMOBProject.Models.Application application)
        {
            InitializeComponent();
            this.Application = application ?? new CIMOBProject.Models.Application();
            stateCombo.ItemsSource = _db.ApplicationStats.ToList();
            stateCombo.DisplayMemberPath = "SubjectName";
            stateCombo.SelectedValuePath = "Id";
            GridFormApplication.DataContext = Application;
            mayClose = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            mayClose = true;
            this.Application.ApplicationStatId = (int)stateCombo.SelectedValue;
            if (!UpdateBilateralProtocol())
            {
                mayClose = false;
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            mayClose = true;
        }

        private bool UpdateBilateralProtocol()
        {
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !mayClose;
        }
    }
}
