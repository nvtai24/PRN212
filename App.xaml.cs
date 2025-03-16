using System.Configuration;
using System.Data;
using System.Windows;
using PRN212.ViewModels;
using PRN212.Views;

namespace PRN212;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        LoginWindow loginWindow = new();
        loginWindow.Show(); 
    }
    
}