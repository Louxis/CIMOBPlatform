using BackOfficeWPF.Dialogs;
using CIMOBProject.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace BackOfficeWPF.Screens {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the subjects.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class SubjectScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        private ObservableCollection<CollegeSubject> subjects = new ObservableCollection<CollegeSubject>();

        public SubjectScreen()
        {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            subjectGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            subjectGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            subjectGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            subjectGrd.Items.MoveCurrentToLast();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) {
            CollegeSubjectDialog collegeSubjectDialog = new CollegeSubjectDialog() { Title = "Adicionar Curso" };
            if (collegeSubjectDialog.ShowDialog() == true) {
                subjects.Add(DbContextHelper.AddCollegeSubject(_db, collegeSubjectDialog.CollegeSubject));
                subjectGrd.SelectedIndex = subjectGrd.Items.Count - 1;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            CollegeSubject employeeEdit = subjectGrd.SelectedItem as CollegeSubject;
            if (employeeEdit == null)
                return;
            CollegeSubjectDialog collegeSubjectDialog = new CollegeSubjectDialog(new CollegeSubject(employeeEdit)) { Title = "Editar Curso" };
            if (collegeSubjectDialog.ShowDialog() == true) {
                DbContextHelper.EditSubject(_db, collegeSubjectDialog.CollegeSubject);
                subjectGrd.Items.Refresh();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.CollegeSubjects.Load();
            subjects = _db.CollegeSubjects.Local;
            subjectGrd.ItemsSource = subjects;
            subjectGrd.SelectedIndex = 0;
            subjectGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
