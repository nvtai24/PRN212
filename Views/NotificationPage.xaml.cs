using PRN212.Models;
using PRN212.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PRN212.Views;

public partial class NotificationPage : Page
{
    public ObservableCollection<Notification> MyNotifications { get; set; }

    public NotificationPage()
    {
        InitializeComponent();
        MyNotifications = new ObservableCollection<Notification>();
        LoadNotifications();
        this.DataContext = this;
    }

    private void LoadNotifications()
    {
        MyNotifications.Clear(); // Clear existing notifications to avoid duplicates

        var user = Application.Current.Properties["User"] as User;
        if (user != null)
        {
            List<Notification> notifications = NotificationRepository.ListMyNoti(user.UserId);
            foreach (var notification in notifications)
            {
                MyNotifications.Add(notification);
            }
        }
    }


    private void IsReadCb_Checked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var notification = checkBox?.DataContext as Notification;

        MessageBox.Show("cc " + notification.NotificationId + " " + notification.IsRead);

        if (notification != null)
        {
            NotificationRepository.ToggleIsRead(notification.NotificationId);
        }
    }


    private void MarkAllAsReadBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var user = Application.Current.Properties["User"] as User;
        if (user != null)
        {
            NotificationRepository.MarkReadAll(user.UserId);
            LoadNotifications();
        }
    }
}