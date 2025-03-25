using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN212.Models;

namespace PRN212.Repositories
{
    public class UserRepository
    {
        private Prn212Context _context;

        public UserRepository()
        {
            _context = new Prn212Context();
        }

        public bool ValidateUser(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }

        public void AddUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public bool IsUserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }


        public static List<User> ListUsers()
        {
            using (var context = new Prn212Context())
            {
                return context.Users.Where(u => u.Role != "Admin").ToList();
            }
        }


        public static List<User> FilterUsers(string keyword, string role, int status)
        {
            using (var db = new Prn212Context())
            {
                var query = db.Users.AsQueryable();

                if (keyword != null)
                {
                    keyword = keyword.ToLower();
                    query = query.Where(u =>
                        u.FullName.Contains(keyword) || u.Address.Contains(keyword) || u.Email.Contains(keyword) ||
                        u.Phone.Contains(keyword));
                }

                if (role != "All")
                {
                    query = query.Where(u => u.Role == role);
                }

                if (status != -1)
                {
                    var isActive = status == 1;
                    query = query.Where(u => u.Status == isActive);
                }

                return query
                    .Where(u => u.Role != "Admin")
                    .ToList();
            }
        }

        public static void ToggleStatus(User u)
        {
            using (var db = new Prn212Context())
            {
                u.Status = !u.Status;

                db.Update(u);
                db.SaveChanges();
            }
        }


        public static void UpdateUser(User u)
        {
            using (var db = new Prn212Context())
            {
                db.Update(u);
                db.SaveChanges();
            }
        }

        public static void AddUserWindow(User u)
        {
            using (var db = new Prn212Context())
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
        }

        public static User CheckIfUserExists(string email, string phone)
        {
            using (var db = new Prn212Context())
            {
                return db.Users.FirstOrDefault(u1 => u1.Email == email || u1.Phone == phone) ?? null;
            }
        }

    }
}