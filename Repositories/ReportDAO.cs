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

        public void SendReport(Report report)
        {
            Prn212Context context = new Prn212Context();
            context.Reports.Add(report);
            context.SaveChanges();
        }

        public List<Vehicle> GetPlates()
        {
            Prn212Context context = new Prn212Context();
            var vehicles = context.Vehicles.ToList();
            return vehicles;
        }

        public void UpdateReport(Report report)
        {
            Prn212Context context = new Prn212Context();
            var r = context.Reports
                .Where(rp => rp.ReportId == report.ReportId)
                .FirstOrDefault();
            if (r != null)
            {
                r.Location = report.Location;
                r.ViolationType = report.ViolationType;
                r.PlateNumber = report.PlateNumber;
                r.Description = report.Description;
                r.ImageUrl = report.ImageUrl;
                r.VideoUrl = report.VideoUrl;

                context.Reports.Update(r);
                context.SaveChanges();
            }

        }
    }
}
