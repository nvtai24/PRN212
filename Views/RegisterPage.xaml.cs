using System.Windows;
using System.Windows.Controls;
using PRN212.Models;
using PRN212.Repositories;

namespace PRN212.Views;

public partial class RegisterPage : Page
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void BtnRegister_OnClick(object sender, RoutedEventArgs e)
    {
        string fullName = txtFullName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Password.Trim();
        string confirmPassword = txtConfirmPassword.Password.Trim();
        string phone = txtPhone.Text.Trim(); 
        string address = txtAddress.Text.Trim(); 

        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
            string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
        {
            MessageBox.Show("Please fill in all the fields.", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (password != confirmPassword)
        {
            MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!IsValidEmail(email))
        {
            MessageBox.Show("Invalid email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        UserRepository userDao = new UserRepository();
        if (userDao.IsUserExists(email))
        {
            MessageBox.Show("Email already exists. Please choose another email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        User newUser = new User
        {
            FullName = fullName,
            Email = email,
            Password = password,
            Phone = phone,  
            Address = address, 
            Role = "Citizen"
        };

        userDao.AddUser(newUser);

        MessageBox.Show("Registration successful!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

        this.NavigationService.Navigate(new LoginPage());
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mail = new System.Net.Mail.MailAddress(email);
            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}