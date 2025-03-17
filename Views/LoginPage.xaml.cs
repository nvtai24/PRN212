using System.Windows;
using System.Windows.Controls;
using PRN212.Repositories;

namespace PRN212.Views;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        string email = txtUsername.Text;
        string password = txtPassword.Password;
        if (string.IsNullOrEmpty(email))
        {
            MessageBox.Show("Please enter email.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter password.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        UserRepository userDao = new UserRepository();
        bool isValidUser = userDao.ValidateUser(email, password);
        if (isValidUser)
        {
            var user = userDao.GetUserByEmail(email);

            Application.Current.Properties["User"] = user;
            var currentWindow = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            currentWindow?.Close();
        }
        else
        {
            MessageBox.Show("Wrong username or password", "Error login", MessageBoxButton.OK, MessageBoxImage.Error);
        }

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