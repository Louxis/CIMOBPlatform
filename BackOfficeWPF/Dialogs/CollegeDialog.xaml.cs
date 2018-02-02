using CIMOBProject.Models;
using System.Windows;

namespace BackOfficeWPF.Dialogs {
    /// <summary>
    /// Interaction logic for CollegeDialog.xaml
    /// </summary>
    public partial class CollegeDialog : Window
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private bool mayClose;
        public College College { get; set; }

        public CollegeDialog(College college = null)
        {
            InitializeComponent();
            this.College = college ?? new College();
            GridFormCollege.DataContext = College;
            mayClose = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            mayClose = true;
            if (!UpdateCollege())
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

        private bool UpdateCollege()
        {
            if (College.CollegeName == null || College.CollegeName.Equals(""))
            {
                MessageBox.Show("O campo designação é obrigatório.", "Erro - Designação inválida");
                return false;
            }
            if (College.CollegeAlias == null || College.CollegeAlias.Equals(""))
            {
                MessageBox.Show("O campo sigla é obrigatório.", "Erro - Sigla inválida");
                return false;
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !mayClose;
        }
    }
}
