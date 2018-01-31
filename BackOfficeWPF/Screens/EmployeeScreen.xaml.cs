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
using System.Windows.Shapes;

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the employees.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class EmployeeScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public EmployeeScreen() {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            employeesGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            employeesGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            employeesGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            employeesGrd.Items.MoveCurrentToLast();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) {
            EmployeeDialog employeeDialog = new EmployeeDialog() { Title = "Adicionar Funcionário" };
            if (employeeDialog.ShowDialog() == true) {
                employees.Add(DbContextHelper.AddEmployee(_db, employeeDialog.Employee));
                employeesGrd.SelectedIndex = employeesGrd.Items.Count - 1;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            Employee employeeEdit = employeesGrd.SelectedItem as Employee;
            if (employeeEdit == null)
                return;
            EmployeeDialog employeeDialog = new EmployeeDialog(new Employee(employeeEdit)) { Title = "Editar Funcionário" };
            if (employeeDialog.ShowDialog() == true) {
                DbContextHelper.EditEmployee(_db, employeeDialog.Employee);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e) {
            Employee employeeBan = employeesGrd.SelectedItem as Employee;
            if (employeeBan == null)
                return;
            if (MessageBox.Show("Deseja banir o empregado? (S/N)", "Banir empregado?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                _db.Employees.Where(emp => emp.UserName.Equals(employeeBan.UserName)).First().IsBanned = true;
                _db.SaveChanges();
                employeeBan.IsBanned = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.Employees.Load();
            employees = _db.Employees.Local;
            employeesGrd.ItemsSource = employees;
            employeesGrd.SelectedIndex = 0;
            employeesGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
