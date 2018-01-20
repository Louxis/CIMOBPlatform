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
    /// Interaction logic for CreateBilateralDialog.xaml
    /// </summary>
    public partial class BilateralDialog : Window
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private bool mayClose;
        public BilateralProtocol BilateralProtocol { get; set; }

        public BilateralDialog(BilateralProtocol bilateralProtocol = null)
        {
            InitializeComponent();
            this.BilateralProtocol = bilateralProtocol ?? new BilateralProtocol();
            subjectsCombo.ItemsSource = _db.CollegeSubjects.ToList();
            subjectsCombo.DisplayMemberPath = "SubjectName";
            subjectsCombo.SelectedValuePath = "Id";
            GridFormBilateral.DataContext = BilateralProtocol;            
            mayClose = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            mayClose = true;            
            if (!UpdateBilateralProtocol())
            {
                mayClose = false;
            }
            else {
                this.BilateralProtocol.SubjectId = (int)subjectsCombo.SelectedValue;
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            mayClose = true;
        }

        private bool UpdateBilateralProtocol()
        {
            if(BilateralProtocol.Destination == null || BilateralProtocol.Destination.Equals(""))
            {
                MessageBox.Show("O campo destino é obrigatório.", "Erro - Destino inválido");
                return false;
            }
            int result = 0;
            if((!int.TryParse(bilateralSlots.Text, out result)) || BilateralProtocol.OpenSlots < 0)
            {
                MessageBox.Show("O campo vagas é obrigatório e precisa de ser um número positivo.");
                return false;
            }
            if (subjectsCombo.SelectedValue == null) {
                MessageBox.Show("Curso é inválido.", "Erro - Curso inválido");
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
