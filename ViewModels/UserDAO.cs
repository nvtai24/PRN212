using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN212.Models;

namespace PRN212.ViewModels
{
    class UserDAO
    {
        private Prn212Context _context;
        public UserDAO()
        {
            _context = new Prn212Context(); 
        }
        public bool ValidateUser(string email, string password)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null; 
        }

        
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
