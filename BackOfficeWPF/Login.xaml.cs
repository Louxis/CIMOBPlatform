using CIMOBProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

namespace BackOfficeWPF.Auth {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        //change to admin
        private UserManager<Employee> userManager;
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();

        //Concept test: remove on production
        private const string emailAdmin = "admincimob@cimob.admin";
        private const string pwAdmin = "admin12!";

        public Login() {
            userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            //Conecept test:
            if (applicationDbContext.Users.Where(a => a.UserName.Equals(emailAdmin)).FirstOrDefault() == null) {
                Employee user = new Employee {
                    UserName = emailAdmin,
                    Email = emailAdmin,
                    PhoneNumber = "912123456",
                    UserAddress = "Admin Road",
                    PostalCode = "2900-000",
                    EmployeeNumber = "999999999",
                    UserFullname = "Admin Full Name",
                    UserCc = "12345678"
                };
                DbContextHelper.AddAdmin(applicationDbContext, user, pwAdmin);
            }

            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            IdentityUser user = userManager.Find(Email.Text, Password2.Password);
            if (user == null) {
                MessageBox.Show("Dados incorretos.");
            }
            else {
                //go to main window
                if (!userManager.IsInRole(user.Id, "Admin")) {
                    MessageBox.Show("Não tem permissão.");
                }
                else {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
