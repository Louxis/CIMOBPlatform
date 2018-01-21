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

        public Login() {
            userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            var user = userManager.Find(Email.Text, Password.Password);
            if(user == null) {
                MessageBox.Show("Dados incorretos.");
            }
            else {
                //go to main window
            }
        }
    }
}
