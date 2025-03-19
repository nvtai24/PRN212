using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN212.Models;

namespace PRN212.Repositories
{
    class ReportManagerDAO
    {
        public List<Report> GetReports()
        {
            Prn212Context context = new Prn212Context();
            var reports = context.Reports
                .Include(r => r.Reporter)
                .Include(r => r.ProcessedByNavigation)
                .ToList();
            return reports;
        }

        public void ApproveReport(int rid, int processby)
        {
            Prn212Context context = new Prn212Context();
            var report = context.Reports
                .Where(r => r.ReportId == rid)
                .FirstOrDefault();

            if(report != null)
            {
                report.ProcessedBy = processby;
                report.Status = "Approved";

                context.Reports.Update(report);
                context.SaveChanges();
            }
        }

        public void RejectReport(int rid)
        {
            Prn212Context context = new Prn212Context();
            var report = context.Reports
                .Where(r => r.ReportId == rid)
                .FirstOrDefault();

            if (report != null)
            {
                report.Status = "Rejected";

                context.Reports.Update(report);
                context.SaveChanges();
            }
        }

        public Boolean CheckPlate(string plate)
        {
            using (Prn212Context context = new Prn212Context())
            {
                var exists = context.Vehicles.Any(v => v.PlateNumber == plate);

                return !exists;
            }
        }
    }
}
