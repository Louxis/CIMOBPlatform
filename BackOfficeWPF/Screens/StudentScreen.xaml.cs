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

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the students.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class StudentScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        //Can remove this one?
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public StudentScreen()
        {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            studentGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            studentGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            studentGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            studentGrd.Items.MoveCurrentToLast();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            Student studentEdit = studentGrd.SelectedItem as Student;
            if (studentEdit == null)
                return;
            StudentDialog studentDialog = new StudentDialog(new Student(studentEdit)) { Title = "Editar Funcionário" };
            if (studentDialog.ShowDialog() == true) {
                DbContextHelper.EditStudent(_db, studentDialog.Student);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e) {
            Student studentBan = studentGrd.SelectedItem as Student;
            if (MessageBox.Show("Deseja banir o estudante? (S/N)", "Banir estudante?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                _db.Students.Where(s => s.UserName.Equals(studentBan.UserName)).First().IsBanned = true;
                _db.SaveChanges();
                studentBan.IsBanned = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.Students.Include(s => s.CollegeSubject).Load();
            students = _db.Students.Local;
            studentGrd.ItemsSource = students;
            studentGrd.SelectedIndex = 0;
            studentGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
