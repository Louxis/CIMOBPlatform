using BackOfficeWPF.Dialogs;
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
                EmployeeDialog employeeDialog = new EmployeeDialog();
                if(employeeDialog.ShowDialog() == true) {
                    DbContextHelper.AddEmployee(_db, employeeDialog.Employee);
                    ((EmployeeScreen)contentControl.Content).Refresh();
                }
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                BilateralDialog bilateralDialog = new BilateralDialog();
                if(bilateralDialog.ShowDialog() == true) {
                    DbContextHelper.AddBilateral(_db, bilateralDialog.BilateralProtocol);
                    ((BilateralProtocolScreen)contentControl.Content).Refresh();
                }                
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                CollegeDialog collegeDialog = new CollegeDialog();
                if (collegeDialog.ShowDialog() == true) {
                    DbContextHelper.AddCollege(_db, collegeDialog.College);
                    ((CollegeScreen)contentControl.Content).Refresh();
                }
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                CollegeSubjectDialog collegeSubjectDialog = new CollegeSubjectDialog();
                if (collegeSubjectDialog.ShowDialog() == true) {
                    DbContextHelper.AddCollegeSubject(_db, collegeSubjectDialog.CollegeSubject);
                    ((SubjectScreen)contentControl.Content).Refresh();
                }
            }
            if (currentcontroller == typeof(Statistics))
            {
                return;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Type currentcontroller = contentControl.Content.GetType();
            ItemCollection items = null;
            if (currentcontroller == typeof(EmployeeScreen))
            {
                //Adicionar dialog para editar um employee
                items = ((EmployeeScreen)contentControl.Content).employeesGrd.Items;
                int selectedIndex = ((EmployeeScreen)contentControl.Content).employeesGrd.SelectedIndex;
                var employee = items.CurrentItem;
                var employeeUserName = employee.GetType().GetProperty("UserName").GetValue(employee);
                EmployeeDialog employeeDialog = new EmployeeDialog(_db.Employees.Where(a => a.UserName.Equals(((String)employeeUserName))).FirstOrDefault());
                if(employeeDialog.ShowDialog() == true) {
                    DbContextHelper.EditEmployee(_db, employeeDialog.Employee);
                    ((EmployeeScreen)contentControl.Content).Refresh();
                    ((EmployeeScreen)contentControl.Content).employeesGrd.SelectedIndex = selectedIndex;
                }              
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                items = ((StudentScreen)contentControl.Content).studentGrd.Items;
                int selectedIndex = ((StudentScreen)contentControl.Content).studentGrd.SelectedIndex;
                var student = items.CurrentItem;
                var studentUserName = student.GetType().GetProperty("UserName").GetValue(student);
                StudentDialog studentDialog = new StudentDialog(_db.Students.Where(a => a.UserName.Equals(((String)studentUserName))).FirstOrDefault());
                if (studentDialog.ShowDialog() == true) {
                    DbContextHelper.EditStudent(_db, studentDialog.Student);
                    ((StudentScreen)contentControl.Content).Refresh();
                    ((StudentScreen)contentControl.Content).studentGrd.SelectedIndex = selectedIndex;
                }
            }
            if (currentcontroller == typeof(ApplicationScreen))
            {
                items = ((ApplicationScreen)contentControl.Content).applicationGrd.Items;
                int selectedIndex = ((ApplicationScreen)contentControl.Content).applicationGrd.SelectedIndex;
                var application = items.CurrentItem;
                var applicationId = application.GetType().GetProperty("ApplicationId").GetValue(application);
                ApplicationDialog applicationDialog = new ApplicationDialog(_db.Applications.Where(a => a.ApplicationId == (int)applicationId).FirstOrDefault());
                if (applicationDialog.ShowDialog() == true) {
                    DbContextHelper.EditApplication(_db, applicationDialog.Application);
                    ((ApplicationScreen)contentControl.Content).Refresh();
                    ((ApplicationScreen)contentControl.Content).applicationGrd.SelectedIndex = selectedIndex;
                }
                _db.SaveChanges();
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                items = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                int selectedIndex = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.SelectedIndex;
                var protocol = items.CurrentItem;
                var protocolId = protocol.GetType().GetProperty("Id").GetValue(protocol);
                BilateralDialog protocolDialog = new BilateralDialog(_db.BilateralProtocols.Where(b => b.Id == (int)protocolId).FirstOrDefault());
                if (protocolDialog.ShowDialog() == true) {
                    DbContextHelper.EditBilateral(_db, protocolDialog.BilateralProtocol);
                    ((BilateralProtocolScreen)contentControl.Content).Refresh();
                    ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.SelectedIndex = selectedIndex;
                }
            }
            if (currentcontroller == typeof(CollegeScreen))
            {
                items = ((CollegeScreen)contentControl.Content).collegeGrd.Items;
                int selectedIndex = ((CollegeScreen)contentControl.Content).collegeGrd.SelectedIndex;
                var college = items.CurrentItem;
                var collegeId = college.GetType().GetProperty("Id").GetValue(college);
                CollegeDialog collegeDialog = new CollegeDialog(_db.Colleges.Where(c => c.Id == (int)collegeId).FirstOrDefault());
                if (collegeDialog.ShowDialog() == true) {
                    DbContextHelper.EditCollege(_db, collegeDialog.College);
                    ((CollegeScreen)contentControl.Content).Refresh();
                    ((CollegeScreen)contentControl.Content).collegeGrd.SelectedIndex = selectedIndex;
                }
            }
            if (currentcontroller == typeof(SubjectScreen))
            {
                items = ((SubjectScreen)contentControl.Content).subjectGrd.Items;
                int selectedIndex = ((SubjectScreen)contentControl.Content).subjectGrd.SelectedIndex;
                var subject = items.CurrentItem;
                var subjectId = subject.GetType().GetProperty("Id").GetValue(subject);
                CollegeSubjectDialog collegeSubjectDialog = new CollegeSubjectDialog(_db.CollegeSubjects.Where(c => c.Id == (int)subjectId).FirstOrDefault());
                if (collegeSubjectDialog.ShowDialog() == true) {
                    DbContextHelper.EditSubject(_db, collegeSubjectDialog.CollegeSubject);
                    ((SubjectScreen)contentControl.Content).Refresh();
                    ((SubjectScreen)contentControl.Content).subjectGrd.SelectedIndex = selectedIndex;
                }
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
                message = "Deseja banir o empregado? (S/N)";
                finalVerification = "Banir empregado?";
            }
            if (currentcontroller == typeof(StudentScreen))
            {
                items = ((StudentScreen)contentControl.Content).studentGrd.Items;
                message = "Deseja banir o aluno? (S/N)";
                finalVerification = "Banir aluno?";
            }
            if (currentcontroller == typeof(BilateralProtocolScreen))
            {
                items = ((BilateralProtocolScreen)contentControl.Content).bilateralGrd.Items;
                message = "Deseja remover este acordo da lista de escolhas? (S/N)";
                finalVerification = "Remover acordo bilateral?";
            }
            if (currentcontroller == typeof(NewsScreen))
            {
                items = ((NewsScreen)contentControl.Content).newsGrd.Items;
                message = "Deseja remover esta Noticia? (S/N)";
                finalVerification = "Remover esta Noticia?";
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
                    contentControl.Content = new EmployeeScreen();
                }
                if (currentcontroller == typeof(StudentScreen))
                {
                    var student = items.CurrentItem;
                    var studentName = student.GetType().GetProperty("UserName").GetValue(student);

                    _db.Students.Where(a => a.UserName.Equals(((String)studentName))).First().IsBanned = true;
                    ((StudentScreen)contentControl.Content).Refresh();
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
                _db.SaveChanges();
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
