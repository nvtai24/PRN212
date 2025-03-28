﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN212.Models;

namespace PRN212.Repositories
{
    class ReportViolation
    {
        public List<Report> GetReportsById(int UserId)
        {
            using (var context = new Prn212Context())
            {
                var reports = context.Reports
                    .Include(r => r.ProcessedByNavigation)
                    .Include(r => r.Violations)
                    .Where(r => r.Violations.Any(v => v.ViolatorId == UserId))
                    .ToList();
                return reports;
            }
        }


        public void SendReport(Report report)
        {
            Prn212Context context = new Prn212Context();
            context.Reports.Add(report);
            context.SaveChanges();
        }

        public void SendReport2(Report report, Notification notice)
        {
            using (var context = new Prn212Context())
            {
                context.Reports.Add(report);
                context.Notifications.Add(notice);
                context.SaveChanges();
            }
        }


        public List<Vehicle> GetPlates(int myId)
        {
            Prn212Context context = new Prn212Context();
            var vehicles = context.Vehicles
                .Where(v => v.OwnerId != myId)
                .ToList();
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
