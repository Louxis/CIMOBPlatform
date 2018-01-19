using BackOfficeWPF.Screens;
using CIMOBProject.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BackOfficeWPF {
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
            ToolBar.Visibility = Visibility.Collapsed;
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            DataGrid grid = null;
            ItemCollection itemList = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                ((EmployeeScreen)contentControl.Content).employeesGrd.Items.MoveCurrentToFirst();
                itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                grid = ((EmployeeScreen)contentControl.Content).employeesGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                ((StudentScreen)contentControl.Content).studentGrd.Items.MoveCurrentToFirst();
                itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;
                grid = ((StudentScreen)contentControl.Content).studentGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items.MoveCurrentToFirst();
                itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                grid = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ((NewsScreen)contentControl.Content).newsGrd.Items.MoveCurrentToFirst();
                itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;
                grid = ((NewsScreen)contentControl.Content).newsGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                ((CollegeScreen)contentControl.Content).collegeGrd.Items.MoveCurrentToFirst();
                itemList = ((CollegeScreen)contentControl.Content).collegeGrd.Items;
                grid = ((CollegeScreen)contentControl.Content).collegeGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                ((SubjectScreen)contentControl.Content).subjectGrd.Items.MoveCurrentToFirst();
                itemList = ((SubjectScreen)contentControl.Content).subjectGrd.Items;
                grid = ((SubjectScreen)contentControl.Content).subjectGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }

        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            DataGrid grid = null;
            ItemCollection itemList = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((EmployeeScreen)contentControl.Content).employeesGrd;
                grid.ScrollIntoView(itemList.CurrentItem);

            }
            if (currentcontroller == typeof(StudentScreen))
            {
                itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;
                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((StudentScreen)contentControl.Content).studentGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((NewsScreen)contentControl.Content).newsGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                itemList = ((CollegeScreen)contentControl.Content).collegeGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((CollegeScreen)contentControl.Content).collegeGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                itemList = ((SubjectScreen)contentControl.Content).subjectGrd.Items;

                if (!itemList.MoveCurrentToPrevious())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((SubjectScreen)contentControl.Content).subjectGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            DataGrid grid = null;
            ItemCollection itemList = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((EmployeeScreen)contentControl.Content).employeesGrd;
                grid.ScrollIntoView(itemList.CurrentItem);

            }
            if (currentcontroller == typeof(StudentScreen))
            {
                itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;
                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((StudentScreen)contentControl.Content).studentGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((NewsScreen)contentControl.Content).newsGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                itemList = ((CollegeScreen)contentControl.Content).collegeGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((CollegeScreen)contentControl.Content).collegeGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                itemList = ((SubjectScreen)contentControl.Content).subjectGrd.Items;

                if (!itemList.MoveCurrentToNext())
                {
                    itemList.MoveCurrentToLast();
                }
                grid = ((SubjectScreen)contentControl.Content).subjectGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            DataGrid grid = null;
            ItemCollection itemList = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                ((EmployeeScreen)contentControl.Content).employeesGrd.Items.MoveCurrentToLast();
                itemList = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                grid = ((EmployeeScreen)contentControl.Content).employeesGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                ((StudentScreen)contentControl.Content).studentGrd.Items.MoveCurrentToLast();
                itemList = ((StudentScreen)contentControl.Content).studentGrd.Items;
                grid = ((StudentScreen)contentControl.Content).studentGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items.MoveCurrentToLast();
                itemList = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                grid = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                ((NewsScreen)contentControl.Content).newsGrd.Items.MoveCurrentToLast();
                itemList = ((NewsScreen)contentControl.Content).newsGrd.Items;
                grid = ((NewsScreen)contentControl.Content).newsGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                ((CollegeScreen)contentControl.Content).collegeGrd.Items.MoveCurrentToLast();
                itemList = ((CollegeScreen)contentControl.Content).collegeGrd.Items;
                grid = ((CollegeScreen)contentControl.Content).collegeGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                ((SubjectScreen)contentControl.Content).subjectGrd.Items.MoveCurrentToLast();
                itemList = ((SubjectScreen)contentControl.Content).subjectGrd.Items;
                grid = ((SubjectScreen)contentControl.Content).subjectGrd;
                grid.ScrollIntoView(itemList.CurrentItem);
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

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Visible;
            ButtonEdit.Visibility = Visibility.Visible;
        }

        private void ButtonProtocol(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new BilateralProtocolScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Visible;
            ButtonEdit.Visibility = Visibility.Visible;
        }

        private void ButtonStudent(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new StudentScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Collapsed;
            ButtonEdit.Visibility = Visibility.Visible;
        }
        private void ButtonNews(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new NewsScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Collapsed;
            ButtonEdit.Visibility = Visibility.Visible;
        }

        private void ButtonApplicationScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ApplicationScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Collapsed;
            ButtonAdd.Visibility = Visibility.Collapsed;
            ButtonEdit.Visibility = Visibility.Visible;
        }

        private void ButtonCollegeScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new CollegeScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Collapsed;
            ButtonAdd.Visibility = Visibility.Visible;
            ButtonEdit.Visibility = Visibility.Visible;

        }

        private void ButtonSubjectScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new SubjectScreen();

            ToolBar.Visibility = Visibility.Visible;

            ButtonRemove.Visibility = Visibility.Collapsed;
            ButtonAdd.Visibility = Visibility.Visible;
            ButtonEdit.Visibility = Visibility.Visible;

        }

        private void ButtonMainScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Statistics();
            ToolBar.Visibility = Visibility.Collapsed;

        }
    }
}
