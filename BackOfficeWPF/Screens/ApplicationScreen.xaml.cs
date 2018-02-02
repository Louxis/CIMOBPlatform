using BackOfficeWPF.Dialogs;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace BackOfficeWPF.Screens {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the student applications.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class ApplicationScreen : UserControl {

        private ApplicationDbContext _db = new ApplicationDbContext();

        public ApplicationScreen()
        {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            applicationGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            applicationGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            applicationGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            applicationGrd.Items.MoveCurrentToLast();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            CIMOBProject.Models.Application applicationEdit = applicationGrd.SelectedItem as CIMOBProject.Models.Application;
            if (applicationEdit == null)
                return;
            ApplicationDialog applicationDialog = new ApplicationDialog(new CIMOBProject.Models.Application(applicationEdit)) { Title = "Editar Estado Candidatura" };
            if (applicationDialog.ShowDialog() == true) {
                DbContextHelper.EditApplication(_db, applicationDialog.Application);
                //Refresh needed to update virtual properties.
                applicationGrd.Items.Refresh();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.Applications.Load();
            applicationGrd.ItemsSource = _db.Applications.Local;
            applicationGrd.IsSynchronizedWithCurrentItem = true;
            applicationGrd.SelectedIndex = 0;
        }
    }
}
