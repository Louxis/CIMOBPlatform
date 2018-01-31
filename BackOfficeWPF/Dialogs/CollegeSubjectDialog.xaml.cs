using CIMOBProject.Models;
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

namespace BackOfficeWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for CollegeSubjectDialog.xaml
    /// </summary>
    public partial class CollegeSubjectDialog : Window
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private bool mayClose;
        public CollegeSubject CollegeSubject { get; set; }

        public CollegeSubjectDialog(CollegeSubject collegeSubject = null)
        {
            InitializeComponent();
            this.CollegeSubject = collegeSubject ?? new CollegeSubject();
            GridFormCollegeSubject.DataContext = CollegeSubject;
            collegesCombo.ItemsSource = _db.Colleges.ToList();
            collegesCombo.DisplayMemberPath = "CollegeName";
            collegesCombo.SelectedValuePath = "Id";
            mayClose = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            mayClose = true;            
            if (!UpdateCollegeSubject())
            {
                mayClose = false;
            }
            else
            {
                this.CollegeSubject.CollegeId = (int)collegesCombo.SelectedValue;
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            mayClose = true;
        }

        private bool UpdateCollegeSubject()
        {
            if (CollegeSubject.SubjectName == null || CollegeSubject.SubjectName.Equals(""))
            {
                MessageBox.Show("O campo designação é obrigatório.", "Erro - Designação inválida");
                return false;
            }
            if (CollegeSubject.SubjectAlias == null || CollegeSubject.SubjectAlias.Equals(""))
            {
                MessageBox.Show("O campo sigla é obrigatório.", "Erro - Sigla inválida");
                return false;
            }
            if(collegesCombo.SelectedValue == null)
            {
                MessageBox.Show("Escola é inválida.", "Erro - Escola inválida");
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
