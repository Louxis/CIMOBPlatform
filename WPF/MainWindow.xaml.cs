using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF {

    public partial class MainWindow : Window {
        //private ObservableCollection<Empresa> empresas;
        //private Empresa empresaAtual;
        ApplicationDbContext _db = new ApplicationDbContext();

        public MainWindow()
        {
            InitializeComponent();

            //empresas = new ObservableCollection<Empresa>
            //{
            //    new Empresa(1, "Auto Europa", "AUTOE", 29),
            //    new Empresa(2, "Hidro Sado", "HIDROSA", 2),
            //    new Empresa(3, "Sapec", "SAPEC", 18),
            //    new Empresa(4, "IPS", "IPS", 25)
            //};

            if (!_db.Empresas.Any())
            {
                _db.Empresas.Local.Add(new Empresa(1, "Auto Europa", "AUTOE", 29));
                _db.Empresas.Local.Add(new Empresa(2, "Hidro Sado", "HIDROSA", 2));
                _db.Empresas.Local.Add(new Empresa(3, "Sapec", "SAPEC", 18));
                _db.Empresas.Local.Add(new Empresa(4, "IPS", "IPS", 25));
                _db.SaveChanges();
            }

            //empresas = new ObservableCollection<Empresa>(_db.Empresas.Local);

            empresaAtual = _db.Empresas.FirstOrDefault();


            //foreach (Empresa empresa in empresas)
            //{
            //    ListBoxEmpresas.Items.Add(empresa);
            //}

            //ListBoxEmpresas.ItemsSource = empresas;
            ListBoxEmpresas.ItemsSource = _db.Empresas.Local;
            ListBoxEmpresas.DisplayMemberPath = "Nome";
            ListBoxEmpresas.IsSynchronizedWithCurrentItem = true;
            AtualizarControlos();
        }

        private void AtualizarControlos()
        {
            TxtSigla.Text = empresaAtual.Sigla;
            TxtNome.Text = empresaAtual.Nome;
            TxtQuantidade.Text = empresaAtual.Quantidade.ToString();

            AtualizarEstado();
        }

        private void AtualizarEstado()
        {
            LabelEmpresa.Content = string.Format("Ficha {0} de {1}", ListBoxEmpresas.Items.CurrentPosition + 1, ListBoxEmpresas.Items.Count);
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmpresas.Items.MoveCurrentToFirst();
            if (ListBoxEmpresas.Items.CurrentItem != null)
            {
                empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
                AtualizarControlos();
            }
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (!ListBoxEmpresas.Items.MoveCurrentToPrevious())
                ListBoxEmpresas.Items.MoveCurrentToLast();
            if (ListBoxEmpresas.Items.CurrentItem != null)
            {
                empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
                AtualizarControlos();
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (!ListBoxEmpresas.Items.MoveCurrentToNext())
                ListBoxEmpresas.Items.MoveCurrentToFirst();
            if (ListBoxEmpresas.Items.CurrentItem != null)
            {
                empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
                AtualizarControlos();
            }

        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmpresas.Items.MoveCurrentToLast();
            if (ListBoxEmpresas.Items.CurrentItem != null)
            {
                empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
                AtualizarControlos();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditarEmpresaDialog empresaDlg = new EditarEmpresaDialog(_db.Empresas) { Title = "Nova Empresa" };

            if (empresaDlg.ShowDialog() == true)
            {
                empresaAtual = empresaDlg.Empresa;

                //empresas.Add(empresaAtual);
                _db.Empresas.Local.Add(empresaAtual);
                _db.SaveChanges();

                //ListBoxEmpresas.Items.Add(empresaAtual);
                //ListBoxEmpresas.Items.MoveCurrentToLast();

                AtualizarControlos();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (empresaAtual == null)
                return;

            EditarEmpresaDialog dlg = new EditarEmpresaDialog(_db.Empresas, new Empresa(empresaAtual)) { Title = "Editar Empresa" };

            if (dlg.ShowDialog() == true && dlg.Empresa != empresaAtual)
            {
                empresaAtual.Nome = dlg.Empresa.Nome;
                empresaAtual.Sigla = dlg.Empresa.Sigla;
                empresaAtual.Quantidade = dlg.Empresa.Quantidade;

                _db.Entry(empresaAtual).State = EntityState.Modified;
                _db.SaveChanges();

                ListBoxEmpresas.Items.Refresh();
                AtualizarControlos();
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if ((!_db.Empresas.Any()) || ListBoxEmpresas.SelectedItem == null)
                return;

            empresaAtual = ListBoxEmpresas.SelectedItem as Empresa;

            if (MessageBox.Show("Deseseja mesmo apagar o empresa (Y/N)?", "Apagar Empresa?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //empresas.Remove(empresaAtual);
                _db.Empresas.Local.Remove(empresaAtual);
                ListBoxEmpresas.ItemsSource = _db.Empresas.Local;

                //ListBoxEmpresas.Items.Remove(empresaAtual);
                ListBoxEmpresas.Items.MoveCurrentToFirst();

                if (_db.Empresas.Any())
                    empresaAtual = _db.Empresas.FirstOrDefault();
                else
                    empresaAtual = new Empresa();

                AtualizarControlos();
            }
        }

        private void ListBoxEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxEmpresas.Items.CurrentItem != null)
            {
                empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
                AtualizarControlos();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _db.Empresas.Load();
            ListBoxEmpresas.ItemsSource = _db.Empresas.Local;
            //empresaAtual = _db.Empresas.Local.FirstOrDefault();
        }
    }
}
