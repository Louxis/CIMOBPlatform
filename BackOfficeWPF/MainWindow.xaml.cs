using CIMOBProject.Models;
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
            //this.DataContext = _db.Users.Local;
            this.contentControl.Content = new Statistics();
            ButtonRemove.Visibility = Visibility.Hidden;
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
            Type currentcontroller = contentControl.Content.GetType();

            if(currentcontroller == typeof(EmployeeScreen))
            {
                ((EmployeeScreen)contentControl.Content).employeesGrd.Items.MoveCurrentToFirst();
            }
            if(currentcontroller == typeof(StudentScreen))
            {
                ((StudentScreen)contentControl.Content).studentGrd.Items.MoveCurrentToFirst();
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items.MoveCurrentToFirst();
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ((NewsScreen)contentControl.Content).newsGrd.Items.MoveCurrentToFirst();
            }

        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();

            if (currentcontroller == typeof(EmployeeScreen))
            {
                ItemCollection itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                if(!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToFirst();
                }

            }
            if (currentcontroller == typeof(StudentScreen))
            {
                ItemCollection itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToFirst();
                }
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ItemCollection itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToFirst();
                }
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ItemCollection itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToFirst();
                }
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();

            if (currentcontroller == typeof(EmployeeScreen))
            {
                ItemCollection itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }

            }
            if (currentcontroller == typeof(StudentScreen))
            {
                ItemCollection itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;
                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ItemCollection itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ItemCollection itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
            }
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();

            if (currentcontroller == typeof(EmployeeScreen))
            {
                ((EmployeeScreen)contentControl.Content).employeesGrd.Items.MoveCurrentToLast();
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                ((StudentScreen)contentControl.Content).studentGrd.Items.MoveCurrentToLast();
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items.MoveCurrentToLast();
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ((NewsScreen)contentControl.Content).newsGrd.Items.MoveCurrentToLast();
            }
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
            

            Type currentcontroller = contentControl.Content.GetType();
            ItemCollection items = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                items = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                items = ((StudentScreen)contentControl.Content).studentGrd.Items;
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                
                items = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                items = ((NewsScreen)contentControl.Content).newsGrd.Items;
            }
            if (currentcontroller == typeof(Statistics))
            {
                return;
            }

            if (items.CurrentItem == null || items.Count == 0)
                return;


            if (MessageBox.Show("Deseseja mesmo campo selecionado (Y/N)?", "Apagar campo?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(currentcontroller == typeof(EmployeeScreen))
                {
                    var employee = items.CurrentItem;
                    var employeeId = employee.GetType().GetProperty("UserName").GetValue(employee);
                    _db.Employees.Where(a => a.UserName.Equals(((String)employeeId))).First().IsBanned = true;
                    _db.SaveChanges();
                    contentControl.Content = new EmployeeScreen();
                }
                if (currentcontroller == typeof(StudentScreen))
                {
                    var student = items.CurrentItem;
                    var studentId = student.GetType().GetProperty("UserName").GetValue(student);

                    _db.Students.Where(a => a.StudentNumber.Equals(((String)studentId))).First().IsBanned = true;
                    _db.SaveChanges();
                    contentControl.Content = new StudentScreen();
                }
                if (currentcontroller == typeof(BilateralProtocolScreen))
                {
                    var bilateralProtocol = items.CurrentItem;
                    var bilateralId = bilateralProtocol.GetType().GetProperty("Destination").GetValue(bilateralProtocol);
                    var bilateralSubject = bilateralProtocol.GetType().GetProperty("SubjectName").GetValue(bilateralProtocol);
                    //var bilateralId = bilateralProtocol.GetType().GetProperty("Id").GetValue(bilateralProtocol);
                    //_db.BilateralProtocols.Where(a => a.Id == ((int)bilateralId)).First().OpenSlots = -1;
                    _db.BilateralProtocols.Where(a => a.Destination.Equals((String)bilateralId) &&
                                                a.Subject.SubjectName.Equals((String)bilateralSubject)).First().OpenSlots = 99;
                    _db.SaveChanges();
                    contentControl.Content = new BilateralProtocolScreen();
                }
                if (currentcontroller == typeof(NewsScreen))
                {
                    var news = items.CurrentItem;
                    var newsTitle = news.GetType().GetProperty("Title").GetValue(news);
                    var newsContent = news.GetType().GetProperty("TextContent").GetValue(news);
                    News selectedNews = _db.News.SingleOrDefault(n => n.Title.Equals((String)newsTitle) && n.TextContent.Equals((String)newsContent));
                    _db.News.Remove(selectedNews);
                    contentControl.Content = new NewsScreen();
                }
            }
        }

        private void ListBoxEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (ListBoxEmpresas.Items.CurrentItem != null)
            //{
            //    empresaAtual = ListBoxEmpresas.Items.CurrentItem as Empresa;
            //    AtualizarControlos();
            //}
        }

        private void ButtonEmployee(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new EmployeeScreen();
            if (ButtonRemove.Visibility == Visibility.Hidden)
            {
                ButtonRemove.Visibility = Visibility.Visible;
            }
        }

        private void ButtonProtocol(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new BilateralProtocolScreen();
            if (ButtonRemove.Visibility == Visibility.Hidden)
            {
                ButtonRemove.Visibility = Visibility.Visible;
            }
        }

        private void ButtonStudent(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new StudentScreen();
            if (ButtonRemove.Visibility == Visibility.Hidden)
            {
                ButtonRemove.Visibility = Visibility.Visible;
            }
        }
        private void ButtonNews(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new NewsScreen();
            if(ButtonRemove.Visibility == Visibility.Hidden)
            {
                ButtonRemove.Visibility = Visibility.Visible;
            }
        }

        private void ButtonMainScreens(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Statistics();
            ButtonRemove.Visibility = Visibility.Hidden;
        }


    }
}
