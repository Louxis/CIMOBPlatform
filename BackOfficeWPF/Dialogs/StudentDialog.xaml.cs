using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for StudentDialog.xaml
    /// </summary>
    public partial class StudentDialog : Window
    {

        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private bool mayClose;
        public Student Student { get; set; }


        public StudentDialog(Student student)
        {
            InitializeComponent();
            this.Student = student ?? new Student();
            Student.BirthDate = new DateTime(2000, 1, 1);
            GridFormStudent.DataContext = Student;
            mayClose = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            mayClose = true;
            if (!UpdateStudent())
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

        private bool UpdateStudent()
        {
            if (Student.PhoneNumber == null || Student.PhoneNumber.Length > 12)
            {
                MessageBox.Show("O número de telemóvel é inválido.", "Erro - Telemóvel inválido!");
                return false;
            }
            if (Student.StudentNumber == null || Student.StudentNumber.Length > 9)
            {
                MessageBox.Show("O número de empregado é inválido.", "Erro - Número de empregado inválido!");
                return false;
            }
            if (Student.PostalCode == null || Student.PostalCode.Equals(""))
            {
                MessageBox.Show("O código postal é inválido.", "Erro - Código postal inválido!");
                return false;
            }
            if (Student.UserFullname == null || Student.UserFullname.Equals(""))
            {
                MessageBox.Show("O nome é obrigatório.", "Erro - Nome obrigatório!");
                return false;
            }
            if (Student.Email == null || Student.Email.Equals(""))
            {
                MessageBox.Show("O email é obrigatório.", "Erro - Email obrigatório!");
                return false;
            }
            if (Student.UserCc == null || Student.UserCc.Length > 8 || Student.UserCc.Equals(""))
            {
                MessageBox.Show("O cc é obrigatório.", "Erro - CC obrigatório!");
                return false;
            }
            if (Student.UserAddress == null || Student.UserAddress.Equals(""))
            {
                MessageBox.Show("A morada é obrigatória.", "Erro - Morada obrigatória!");
                return false;
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !mayClose;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void PostalCode(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]{4}[ -]?[0-9]{3}");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
