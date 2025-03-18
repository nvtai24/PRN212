using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PRN212.Models;

namespace PRN212.Views;

public partial class HomePage : Page
{
    public string UserName { get; set; }
    public DateTime CurrentDateTime { get; set; }

    public HomePage()
    {
        InitializeComponent();
        
        if (Application.Current.Properties.Contains("User"))
        {
            User user = Application.Current.Properties["User"] as User;

            if (user != null)
            {
                txtUsername.Text = "Welcome, " + user.FullName;
            }
        }
        CurrentDateTime = DateTime.Now;
        this.DataContext = this;
    }
    
}