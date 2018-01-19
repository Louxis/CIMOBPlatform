using BackOfficeWPF.Dialogs;
using BackOfficeWPF.Screens;
using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
            Type currentcontroller = contentControl.Content.GetType();
            if (currentcontroller == typeof(EmployeeScreen))
            {
                //Adicionar dialog para criar um employee
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                //Adicionar dialog para criar BilateralProtocl
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                //Adicionar dialog para criar News
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                //Adicionar dialog para criar College
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                //Adicionar dialog para criar Subjects
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(Statistics))
            {
                return;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            if (currentcontroller == typeof(EmployeeScreen))
            {
                //Adicionar dialog para editar um employee
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                //Adicionar dialog para editar um estudante
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(ApplicationScreen))
            {
                //Adicionar dialog para editar uma candidatura
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                //Adicionar dialog para editar BilateralProtocl
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                //Adicionar dialog para editar News
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                //Adicionar dialog para editar College
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                //Adicionar dialog para editar Subjects
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(Statistics))
            {
                return;
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            

            Type currentcontroller = contentControl.Content.GetType();
            ItemCollection items = null;
            String message = "";
            String finalVerification = "";
            if (currentcontroller == typeof(EmployeeScreen))
            {
                items = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                message = "Do you wish to ban this employee? (Y/N)";
                finalVerification = "Ban Employee?";
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                items = ((StudentScreen)contentControl.Content).studentGrd.Items;
                message = "Do you wish to ban this student? (Y/N)";
                finalVerification = "Ban Student?";
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                items = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                message = "Do you wish to remove this Bilateral Protocol from the selection pool? (Y/N)";
                finalVerification = "Remove Bilateral Protocol?";
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                items = ((NewsScreen)contentControl.Content).newsGrd.Items;
                message = "Do you wish to remove this News? (Y/N)";
                finalVerification = "Remove this News?";
            }
            if (currentcontroller == typeof(Statistics))
            {
                return;
            }

            if (items.CurrentItem == null || items.Count == 0)
                return;
            

            if (MessageBox.Show(message, finalVerification, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                    var studentName = student.GetType().GetProperty("UserName").GetValue(student);

                    _db.Students.Where(a => a.UserName.Equals(((String)studentName))).First().IsBanned = true;
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

        private void ButtonApplicationScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ApplicationScreen();
            ButtonRemove.Visibility = Visibility.Visible;
        }

        private void ButtonCollegeScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new CollegeScreen();
            ButtonRemove.Visibility = Visibility.Hidden;
        }

        private void ButtonSubjectScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new SubjectScreen();
            ButtonRemove.Visibility = Visibility.Hidden;
            
        }

        private void ButtonMainScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Statistics();
            ButtonRemove.Visibility = Visibility.Hidden;
            
        }
    }
}
