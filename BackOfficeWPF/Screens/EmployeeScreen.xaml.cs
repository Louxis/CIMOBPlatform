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

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the employees.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class EmployeeScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        public EmployeeScreen()
        {
            InitializeComponent();
            employeesGrd.ItemsSource = _db.Employees.Select(e => new { e.UserName, e.UserFullname, e.UserCc, e.Email, e.IsBanned }).ToList();
            employeesGrd.IsSynchronizedWithCurrentItem = true;
        }

        public void Refresh() {
            employeesGrd.ItemsSource = _db.Employees.Select(e => new { e.UserName, e.UserFullname, e.UserCc, e.Email, e.IsBanned }).ToList();
        }

        private void AtualizarControlos()
        {
            //TxtSigla.Text = empresaAtual.Sigla;
            //TxtNome.Text = empresaAtual.Nome;
            //TxtQuantidade.Text = empresaAtual.Quantidade.ToString();

            //AtualizarEstado();
        }

        private void AtualizarEstado()
        {
            //LabelEmpresa.Content = string.Format("Ficha {0} de {1}", ListBoxEmpresas.Items.CurrentPosition + 1, ListBoxEmpresas.Items.Count);
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e)
        {
            //ListBoxEmpresas.Items.MoveCurrentToFirst();
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            //if (!ListBoxEmpresas.Items.MoveCurrentToPrevious())
            //    ListBoxEmpresas.Items.MoveCurrentToLast();
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //if (!ListBoxEmpresas.Items.MoveCurrentToNext())
            //    ListBoxEmpresas.Items.MoveCurrentToFirst();
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}

        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            //ListBoxEmpresas.Items.MoveCurrentToLast();
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //EditarEmpresaDialog empresaDlg = new EditarEmpresaDialog(empresas) { Title = "Nova Empresa" };

            //if (empresaDlg.ShowDialog() == true)
            //{
            //    empresaAtual = empresaDlg.Empresa;

            //    empresas.Add(empresaAtual);

            //    ListBoxEmpresas.Items.Add(empresaAtual);
            //    ListBoxEmpresas.Items.MoveCurrentToLast();

            //    AtualizarControlos();
            //}
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //if (empresaAtual == null)
            //    return;

            //EditarEmpresaDialog dlg = new EditarEmpresaDialog(empresas, new Empresa(empresaAtual)) { Title = "Editar Empresa" };

            //if (dlg.ShowDialog() == true && dlg.Empresa != empresaAtual)
            //{
            //    empresaAtual.Nome = dlg.Empresa.Nome;
            //    empresaAtual.Sigla = dlg.Empresa.Sigla;
            //    empresaAtual.Quantidade = dlg.Empresa.Quantidade;

            //    ListBoxEmpresas.Items.Refresh();
            //    AtualizarControlos();
            //}
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            //if (empresas.Count == 0 || ListBoxEmpresas.SelectedItem == null)
            //    return;

            //empresaAtual = ListBoxEmpresas.SelectedItem as Empresa;

            //if (MessageBox.Show("Deseseja mesmo apagar o empresa (Y/N)?", "Apagar Empresa?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    empresas.Remove(empresaAtual);

            //    ListBoxEmpresas.Items.Remove(empresaAtual);
            //    ListBoxEmpresas.Items.MoveCurrentToFirst();

            //    if (empresas.Count > 0)
            //        empresaAtual = empresas[0];
            //    else
            //        empresaAtual = new Empresa();

            //    AtualizarControlos();
            //}
        }

        private void ListBoxEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}
        }
    }
}
