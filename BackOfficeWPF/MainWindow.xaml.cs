using BackOfficeWPF.Dialogs;
using CIMOBProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            contentControl.Content = new TestControl();
            //this.DataContext = _db.Users.Local;
            /*UserManager<Employee> userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            var user = new Employee
            {
                UserName = "testemployee4@cimob.pt",
                UserFullname = "Empregado Teste3",
                Email = "testemployee4@cimob.pt",
                UserCc = "00000000",
                PhoneNumber = "936930000",
                UserAddress = "RuaTeste2",
                PostalCode = "2900-002",
                BirthDate = new DateTime(1996, 1, 1),
                EmployeeNumber = 150221059,
                NormalizedEmail = "testemployee4@cimob.pt".ToUpper(),
                NormalizedUserName = "testemployee4@cimob.pt".ToUpper()
            };
            userManager.CreateAsync(user, "teste12").Wait();
            _db.SaveChanges();
            var role = _db.Roles.SingleOrDefault(m => m.Name == "Employee");
            userManager.AddToRoleAsync(user.Id, role.Name).Wait();
            _db.SaveChanges();
            _db.Employees
                    .SingleOrDefault(e => e.UserName == user.Email)
                    .EmailConfirmed = true;
            _db.SaveChanges();*/
            employeesGrd.ItemsSource = _db.Employees.Select(s => new { s.UserFullname, s.Email, s.PhoneNumber }).ToList();
            employeesGrd.IsSynchronizedWithCurrentItem = true;
        }

        private void changeContent(object sender, RoutedEventArgs e)
        {
            EmployeeDialog dialog = new EmployeeDialog();
            if (dialog.ShowDialog() == true)
            {
                UserManager<Employee> userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
                var user = new Employee
                {
                    UserName = dialog.Employee.Email,
                    UserFullname = dialog.Employee.UserFullname,
                    Email = dialog.Employee.Email,
                    UserCc = dialog.Employee.UserCc,
                    PhoneNumber = dialog.Employee.PhoneNumber,
                    UserAddress = dialog.Employee.UserAddress,
                    PostalCode = dialog.Employee.PostalCode,
                    BirthDate = dialog.Employee.BirthDate,
                    EmployeeNumber = dialog.Employee.EmployeeNumber,
                    NormalizedEmail = dialog.Employee.Email.ToUpper(),
                    NormalizedUserName = dialog.Employee.Email.ToUpper(),
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user, "teste12").Wait();
                _db.SaveChanges();
                var role = _db.Roles.SingleOrDefault(m => m.Name == "Employee");
                userManager.AddToRoleAsync(user.Id, role.Name).Wait();
                _db.SaveChanges();
                employeesGrd.ItemsSource = _db.Employees.Select(s => new { s.UserFullname, s.Email, s.PhoneNumber, s.EmailConfirmed }).ToList();
            }
        }
    }
}
