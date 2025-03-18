using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using PRN212.Models;
using PRN212.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PRN212.Views;

public partial class SendReportPage : Page
{
    private string selectedImagePath = string.Empty;
    private string selectedVideoPath = string.Empty;

    public SendReportPage()
    {
        InitializeComponent();
        LoadComboBoxLicensePlate();
    }

    void LoadComboBoxLicensePlate()
    {
        ReportDAO reportDAO = new ReportDAO();
        var vehicles = reportDAO.GetPlates();
        this.LicensePlateComboBox.ItemsSource = vehicles;
        this.LicensePlateComboBox.DisplayMemberPath = "PlateNumber";
        this.LicensePlateComboBox.SelectedValuePath = "PlateNumber";
        this.LicensePlateComboBox.SelectedIndex = 0;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Lấy dữ liệu từ các điều khiển
        string location = LocationTextBox.Text;

        string violationType = ViolationTypeTextBox.Text;

        string licensePlate = LicensePlateComboBox.SelectedValue.ToString();
        string description = DescriptionTextBox.Text;

        if (string.IsNullOrEmpty(location))
        {
            MessageBox.Show("Please enter location.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrEmpty(violationType))
        {
            MessageBox.Show("Please enter violation type.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrEmpty(licensePlate))
        {
            MessageBox.Show("Please enter licensePlate.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrEmpty(description))
        {
            MessageBox.Show("Please enter description.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var user = Application.Current.Properties["User"] as User;

        // Tạo đối tượng Report mới
        var report = new Report
        {
            ReporterId = user.UserId, // Bạn cần thay đổi giá trị này phù hợp với ID của người báo cáo
            ViolationType = violationType,
            Description = description,
            PlateNumber = licensePlate,
            Location = location,
            ReportDate = DateTime.Now, // Hoặc sử dụng giá trị date nếu cần
        };

        // Kiểm tra nếu có đường dẫn ảnh được chọn
        if (!string.IsNullOrEmpty(selectedImagePath))
        {
            string imageFileName = Path.GetFileName(selectedImagePath);
            string projectFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");
            string imageFolder = Path.Combine(projectFolder, "Images");

            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            string newImagePath = Path.Combine(imageFolder, imageFileName);
            File.Copy(selectedImagePath, newImagePath, true);

            // Gán đường dẫn ảnh vào đối tượng report
            report.ImageUrl = Path.Combine("Images", imageFileName);
        }
        else
        {
            report.ImageUrl = null;
        }

        // Kiểm tra nếu có đường dẫn video được chọn
        if (!string.IsNullOrEmpty(selectedVideoPath))
        {
            string videoFileName = Path.GetFileName(selectedVideoPath);
            string projectFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");
            string videoFolder = Path.Combine(projectFolder, "Videos");

            if (!Directory.Exists(videoFolder))
            {
                Directory.CreateDirectory(videoFolder);
            }

            string newVideoPath = Path.Combine(videoFolder, videoFileName);
            File.Copy(selectedVideoPath, newVideoPath, true);

            // Gán đường dẫn video vào đối tượng report
            report.VideoUrl = Path.Combine("Videos", videoFileName);
        }
        else
        {
            report.VideoUrl = null;
        }

        // Hiển thị thông báo
        ReportDAO reportDao = new ReportDAO();
        reportDao.SendReport(report);
        MessageBox.Show($"Report submitted successfully!\nLocation: {location}\nViolation Type: {violationType}\nLicense Plate: {licensePlate}\nDescription: {description}\nImage URL: {report.ImageUrl}\nVideo URL: {report.VideoUrl}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }



    private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
    {
        // Mở hộp thoại chọn ảnh
        var fileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
            Title = "Select an Image"
        };

        if (fileDialog.ShowDialog() == true)
        {
            selectedImagePath = fileDialog.FileName;
            SelectedImagePathTextBox.Text = selectedImagePath;

            // Hiển thị preview ảnh
            ImagePreview.Visibility = Visibility.Visible;
            ImagePreview.Source = new BitmapImage(new Uri(selectedImagePath));
        }
    }

    private void ChooseVideoButton_Click(object sender, RoutedEventArgs e)
    {
        // Mở hộp thoại chọn video
        var fileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Video Files|*.mp4;*.avi;*.mkv",
            Title = "Select a Video"
        };

        if (fileDialog.ShowDialog() == true)
        {
            selectedVideoPath = fileDialog.FileName;
            SelectedVideoPathTextBox.Text = selectedVideoPath;

            // Hiển thị preview video
            VideoPreview.Visibility = Visibility.Visible;
            VideoPreview.Source = new Uri(selectedVideoPath);

            // Phát video khi đã chọn
            VideoPreview.Play();
        }
    }



}
