using System.Windows;
using PRN212.Models;

namespace PRN212.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.MainFrame.Content = new HomePage();
        User currentUser = (User)Application.Current.Properties["User"];
        if(currentUser.Role != "TrafficPolice")
        {
            this.btnManageReports.Visibility = Visibility.Collapsed;
        }
        if (currentUser.Role != "Citizen")
        {
            this.btnMyReports.Visibility = Visibility.Collapsed;
            this.btnNotifications.Visibility = Visibility.Collapsed;
            this.btnSendReport.Visibility = Visibility.Collapsed;
            this.btnMyViolations.Visibility = Visibility.Collapsed;
        }
        if (currentUser.Role != "Admin")
        {
            this.btnManageUsers.Visibility = Visibility.Collapsed;
        }

    }
    
    
    private void BtnNotifications_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new NotificationPage();
    }

    private void BtnMyVehicles_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new MyVehiclesPage();
    }

    private void BtnSendReport_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new SendReportPage();
    }

    private void BtnMyReports_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new MyReportPage();
    }

    private void BtnMyViolations_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new MyViolationsPage();
    }

    private void BtnManageUsers_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new ManageUserPage();
    }

    private void BtnManageReports_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new ManageReportPage();
    }

    private void BtnHome_OnClick(object sender, RoutedEventArgs e)
    {
        this.MainFrame.Content = new HomePage();
    }
    private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?",
                                                "Logout Confirmation",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            if (Application.Current.Properties.Contains("User"))
            {
                Application.Current.Properties.Remove("User");
            }

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            this.Close();
        }
    }
}