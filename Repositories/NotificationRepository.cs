using Microsoft.EntityFrameworkCore;
using PRN212.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN212.Repositories
{
    public class NotificationRepository
    {

        public static List<Notification> ListMyNoti(int uid)
        {
            using (var db = new Prn212Context())
            {
                return db.Notifications
                    .Include(n => n.User)
                    .Include(n => n.PlateNumberNavigation)
                                                                    .Where(n => n.UserId == uid)
                    .OrderByDescending(n => n.SentDate)
                    .ToList();
            }
        }

        public static void ToggleIsRead(int nid)
        {
            using (var db = new Prn212Context())
            {
                var n2 = db.Notifications.FirstOrDefault(n => n.NotificationId == nid);

                n2.IsRead = !n2.IsRead;
                db.Update(n2);
                db.SaveChanges();
            }
        }

        public static void MarkReadAll(int userUserId)
        {
            using (var db = new Prn212Context())
            {
                db.Notifications
                    .Where(n => n.UserId == userUserId)
                    .ToList()
                    .ForEach(n => n.IsRead = true);
                
                db.SaveChanges();
            }
        }
    }
}
