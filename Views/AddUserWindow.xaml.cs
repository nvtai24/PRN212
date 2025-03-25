using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using PRN212.Models;
using PRN212.Repositories;

namespace PRN212.Views;

public partial class AddUserWindow : Window
{
    public AddUserWindow()
    {
        InitializeComponent();
        
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    private void AddUserButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ValidateUserInput())
        {
            CreateUser();
        }
    }


    private bool ValidateUserInput()
    {
        if (string.IsNullOrWhiteSpace(NewUserNameTb.Text))
        {
            MessageBox.Show("Fullname is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewUserPhoneTb.Text) || !Regex.IsMatch(NewUserPhoneTb.Text, @"^\d{10,}$"))
        {
            MessageBox.Show("Phone number must be at least 10 digits.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewUserMailTb.Text) ||
            !Regex.IsMatch(NewUserMailTb.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewUserAddressTb.Text))
        {
            MessageBox.Show("Address is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (NewUserRoleCb.SelectedItem == null)
        {
            MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        var user = UserRepository.CheckIfUserExists(NewUserMailTb.Text, NewUserPhoneTb.Text);
        if (user != null)
        {
            MessageBox.Show("Email or phone number already exists.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewUserPasswordTb.Password) ||
            string.IsNullOrWhiteSpace(NewUserConfirmPasswordTb.Password))
        {
            MessageBox.Show("Password and Confirm Password are required.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (!IsPasswordValid(NewUserPasswordTb.Password))
        {
            MessageBox.Show(
                "Password must contain at least 8 characters, including uppercase, lowercase, and a number.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (NewUserPasswordTb.Password != NewUserConfirmPasswordTb.Password)
        {
            MessageBox.Show("Password and Confirm Password do not match.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        return true;
    }

    private bool IsPasswordValid(string password)
    {
        return password.Length >= 8 &&
               Regex.IsMatch(password, @"[A-Z]") && 
               Regex.IsMatch(password, @"[a-z]") &&
               Regex.IsMatch(password, @"[0-9]");
    }

    private void CreateUser()
    {
        string fullname = NewUserNameTb.Text;
        string phone = NewUserPhoneTb.Text;
        string email = NewUserMailTb.Text;
        string address = NewUserAddressTb.Text;
        string role = ((ComboBoxItem)NewUserRoleCb.SelectedItem).Tag.ToString();
        string password = NewUserPasswordTb.Password; 

        User newUser = new User
        {
            FullName = fullname,
            Phone = phone,
            Email = email,
            Address = address,
            Role = role,
            Password = password
        };

        UserRepository.AddUserWindow(newUser);
        MessageBox.Show("User added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();
    }
}