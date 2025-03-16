using System.Windows;
using System.Windows.Controls;

namespace PRN212.Views;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        var currentWindow = Window.GetWindow(this);
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        currentWindow?.Close();

    }
    
    private void Register_OnClick(object sender, RoutedEventArgs e)
    {
        this.NavigationService.Navigate(new RegisterPage());    
    }

    private void ForgotPassword_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}