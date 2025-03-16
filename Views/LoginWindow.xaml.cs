using System.Windows;

namespace PRN212.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        HomeWindow homeWindow = new HomeWindow();
        homeWindow.Show();
        Close();
    }
}