using System.Windows;

namespace PRN212.Views;

public partial class HomeWindow : Window
{
    public HomeWindow()
    {
        InitializeComponent();
    }

    private void BtnNotifications_OnClick(object sender, RoutedEventArgs e)
    {
        MyNotificationWindow notificationWindow = new MyNotificationWindow();
        notificationWindow.Show();  
    }
}