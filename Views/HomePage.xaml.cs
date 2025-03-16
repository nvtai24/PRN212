using System.Windows.Controls;

namespace PRN212.Views;

public partial class HomePage : Page
{
    public string UserName { get; set; }
    public DateTime CurrentDateTime { get; set; }

    public HomePage()
    {
        InitializeComponent();
        UserName = "John Doe"; // Thay bằng tên người dùng thực từ hệ thống
        CurrentDateTime = DateTime.Now;
        this.DataContext = this;
    }
    
}