using BackOfficeWPF.Screens;
using System.Windows;

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
            this.contentControl.Content = new Statistics();
        }

        private void ButtonEmployee(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new EmployeeScreen();
        }

        private void ButtonProtocol(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new BilateralProtocolScreen();
        }

        private void ButtonStudent(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new StudentScreen();
        }
        private void ButtonNews(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new NewsScreen();
        }

        private void ButtonApplicationScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ApplicationScreen();
        }

        private void ButtonCollegeScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new CollegeScreen();
        }

        private void ButtonSubjectScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new SubjectScreen();
        }

        private void ButtonMainScreen(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Statistics();
        }
    }
}
