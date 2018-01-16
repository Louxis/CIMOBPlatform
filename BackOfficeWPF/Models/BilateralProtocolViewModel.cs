using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeWPF.Models
{
    class BilateralProtocolViewModel : IDisposable
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public List<BilateralProtocol> bilateralProtocol { get; set; }

        public List<CollegeSubject> collegeSubject { get; set; }

        public BilateralProtocolViewModel()
        {
            this.bilateralProtocol = _context.BilateralProtocols.ToList();
            this.collegeSubject = _context.CollegeSubjects.ToList();
        }
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
