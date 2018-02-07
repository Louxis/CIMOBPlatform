using BackOfficeWPF.Services;
using CIMOBProject.Models;
using System;
using System.Windows;

namespace BackOfficeWPF.Dialogs {
    /// <summary>
    /// Interaction logic for EmailDialog.xaml
    /// </summary>
    public partial class EmailDialog : Window
    {
        private ApplicationUser user;
        private Employee employee;
        private readonly ApplicationDbContext _db;
        private EmailSender emailSender = new EmailSender();

        public EmailDialog(ApplicationDbContext dbContext, ApplicationUser user)
        {
            InitializeComponent();
            this.user = user;
            this._db = dbContext;
            GridFormEmail.DataContext = user;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            if (ValidateContent()) {
                try {
                    emailSender.Execute(title.Text, message.Text, user.Email);
                } catch(Exception ex) {
                    this.DialogResult = false;
                }
                this.DialogResult = true;
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private bool ValidateContent() {
            if (title.Text.Trim().Equals("")) {
                MessageBox.Show("O campo de título é obrigatório","Erro");
                return false;

            }
            if (message.Text.Trim().Equals("")) {
                MessageBox.Show("O campo de mensagem é obrigatório","Erro");
                return false;
            }
            return true;
        }
    }
}
