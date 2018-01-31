using BackOfficeWPF.Dialogs;
using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BackOfficeWPF.Screens {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the colleges.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class CollegeScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        private ObservableCollection<College> colleges = new ObservableCollection<College>();

        public CollegeScreen()
        {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            collegeGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            collegeGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            collegeGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            collegeGrd.Items.MoveCurrentToLast();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) {
            CollegeDialog collegeDialog = new CollegeDialog() { Title = "Adicionar Universidade" };
            if (collegeDialog.ShowDialog() == true) {
                colleges.Add(DbContextHelper.AddCollege(_db, collegeDialog.College));
                collegeGrd.SelectedIndex = collegeGrd.Items.Count - 1;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            College collegeEdit = collegeGrd.SelectedItem as College;
            if (collegeEdit == null)
                return;
            CollegeDialog collegeDialog = new CollegeDialog(new College(collegeEdit)) { Title = "Editar Universidade" };
            if (collegeDialog.ShowDialog() == true) {
                DbContextHelper.EditCollege(_db, collegeDialog.College);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.Colleges.Load();
            colleges = _db.Colleges.Local;
            collegeGrd.ItemsSource = colleges;
            collegeGrd.SelectedIndex = 0;
            collegeGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
