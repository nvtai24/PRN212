using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PRN212.Models;

namespace PRN212.Repositories
{
    class ViolationDAO
    {
        public Violation GetViolationByReport(int rid)
        {
            Prn212Context context = new Prn212Context();
            var violate = context.Violations
                .Where(v => v.ReportId == rid)
                .FirstOrDefault();

            return violate;
        }
        public void SendViolation(Violation violation, Notification notice)
        {
            try
            {
                using (var context = new Prn212Context())
                {
                    // Thêm các đối tượng vào context
                    context.Violations.Add(violation);
                    context.Notifications.Add(notice);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
