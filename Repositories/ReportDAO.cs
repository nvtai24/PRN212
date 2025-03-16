using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN212.Models;

namespace PRN212.Repositories
{
    class ReportDAO
    {
        public List<Report> GetReportsById(int UserId)
        {
            Prn212Context context = new Prn212Context();
            var reports = context.Reports
                .Where(r => r.ReporterId == UserId).ToList();
            return reports;
        }
    }
}
