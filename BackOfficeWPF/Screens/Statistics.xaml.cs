using System;
using System.Linq;
using System.Windows.Controls;

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the all the statistics that are relevant to the admin.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class Statistics : UserControl
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public Statistics()
        {
            var recentEdital = _db.Editals.ToList();
            InitializeComponent();
            if(recentEdital.Count > 0) {
                DateTime startingDate = recentEdital.Last().OpenDate;
                DateTime closingDate = recentEdital.Last().CloseDate;
                Applications.Content = "" + _db.Applications.Where(a => a.CreationDate >= startingDate && a.CreationDate <= closingDate).Count() + "/" + _db.Applications.Select(a => a.ApplicationId).Count();
            }
            else {
                Applications.Content = "0";
            }

            TotalEmployees.Content = _db.Employees.Count();
            AverageApplication.Content = Math.Round((_db.Applications.Sum(a => a.FinalGrade) / _db.Applications.Select(a => a.FinalGrade).Count()).Value);
        }
    }
}
