using System;
using System.Linq;
using System.Windows.Controls;

namespace BackOfficeWPF {
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public Statistics()
        {
            var recentEdital = _db.Editals.ToList();
            InitializeComponent();
            DateTime startingDate = recentEdital.Last().OpenDate;
            DateTime closingDate = recentEdital.Last().CloseDate;
            TotalEmployees.Content = _db.Employees.Select(e => e.EmployeeNumber).Count();
            Applications.Content = "" + _db.Applications.Where(a => a.CreationDate >= startingDate && a.CreationDate <= closingDate).Count() + "/" + _db.Applications.Select(a => a.ApplicationId).Count();
            AverageApplication.Content = _db.Applications.Sum(a => a.FinalGrade) / _db.Applications.Select(a => a.FinalGrade).Count();
        }
    }
}
