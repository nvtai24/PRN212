using System.Windows;
using System.Windows.Controls;
using PRN212.Models;
using PRN212.Repositories;

namespace PRN212.Views;

public partial class ManageUserPage : Page
{
    public ManageUserPage()
    {
        InitializeComponent();
        LoadUsers();
        RoleComboBox.SelectedIndex = 0;
        StatusComboBox.SelectedIndex = 0;
        UpdateUserStatistics();
    }

    private void LoadUsers()
    {
        this.UsersDataGrid.ItemsSource = UserRepository.ListUsers();
        UpdateUserStatistics();
    }


    private void filterBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var keyword = this.SearchTextBox.Text;

        var roleItem = this.RoleComboBox.SelectedItem as ComboBoxItem;
        var role = roleItem?.Tag.ToString();

        var statusItem = this.StatusComboBox.SelectedItem as ComboBoxItem;
        var status = statusItem != null ? Convert.ToInt32(statusItem.Tag) : -1;

        this.UsersDataGrid.ItemsSource = UserRepository.FilterUsers(keyword, role, status);
    }

    private void exportBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private void ClearFilter()
    {
        this.SearchTextBox.Clear();
        this.RoleComboBox.SelectedIndex = 0;
        this.StatusComboBox.SelectedIndex = 0;
    }


    private void UpdateUserStatistics()
    {
        int totalUsers = UserRepository.ListUsers().Count();
        totalTb.Text = totalUsers.ToString();

        int activeUsers = UserRepository.ListUsers().Count(u => u.Status == true);
        activeTb.Text = activeUsers.ToString();

        int blockedUsers = UserRepository.ListUsers().Count(u => u.Status == false);
        blockedTb.Text = blockedUsers.ToString();
    }

    private void StatusButton_Click(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        if (button == null) return;

        var user = button.DataContext as User;
        if (user == null) return;

        // Toggle trạng thái
        UserRepository.ToggleStatus(user);

        // Làm mới DataGrid
        UsersDataGrid.Items.Refresh();

        // Cập nhật thống kê nếu cần
        UpdateUserStatistics();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {

        var name = this.EditNameTb.Text;
        var phone = this.EditPhoneTb.Text;
        var email = this.EditMailTb.Text;
        var address = this.EditAddressTb.Text;
        var role = this.EditRoleCb.Text;

        User u = new User();
        u.Address = address;
        u.Email = email;
        u.Phone = phone;
        u.FullName = name;
        
         u.Role = role;

        UserRepository.UpdateUser(u);

        ClearFilter();
        LoadUsers();
    }


    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        if (button == null) return;

        var user = button.DataContext as User;
        if (user == null) return;

        this.EditNameTb.Text = user.FullName;
        this.EditPhoneTb.Text = user.Phone;
        this.EditMailTb.Text = user.Email;
        this.EditAddressTb.Text = user.Address;
        this.EditRoleCb.Text = user.Role;
    }
}