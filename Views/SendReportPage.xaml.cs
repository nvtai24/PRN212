using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace PRN212.Views;

public partial class SendReportPage : Page
{
    private string selectedFilePath; // Lưu đường dẫn file tạm thời

    public SendReportPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        // Lấy dữ liệu từ các điều khiển
        string location = LocationTextBox.Text;
        string date = DatePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "Not selected";
        string time = (TimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Not selected";
        string violationType = (ViolationTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Not selected";
        string licensePlate = LicensePlateTextBox.Text;
        string description = DescriptionTextBox.Text;

        // Hiển thị dữ liệu trong MessageBox
        string message = $"Location: {location}\n" +
                         $"Date: {date}\n" +
                         $"Time: {time}\n" +
                         $"Violation Type: {violationType}\n" +
                         $"License Plate: {licensePlate}\n" +
                         $"Description: {description}\n";

        // Kiểm tra nếu có đường dẫn ảnh được chọn
        if (!string.IsNullOrEmpty(selectedFilePath))
        {
            // Lấy tên file từ đường dẫn và thêm vào message
            string fileName = Path.GetFileName(selectedFilePath);
            message += $"Evidence File: {fileName}";

            // Xác định thư mục dựa trên loại file (Ảnh hay Video)
            string projectFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");

            // Kiểm tra loại file (Ảnh hoặc Video)
            string extension = Path.GetExtension(selectedFilePath).ToLower();
            string fileTypeFolder = extension switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" => "Images",
                ".mp4" or ".avi" or ".mkv" => "Videos",
                _ => null
            };

            // Kiểm tra xem có thư mục hợp lệ không
            if (!string.IsNullOrEmpty(fileTypeFolder))
            {
                // Tạo đường dẫn đến thư mục tương ứng
                string fileFolder = Path.Combine(projectFolder, fileTypeFolder);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                string newFilePath = Path.Combine(fileFolder, fileName);

                // Sao chép file vào thư mục tương ứng (Ảnh hoặc Video)
                File.Copy(selectedFilePath, newFilePath, true);
            }
            else
            {
                message += "\nInvalid file type.";
            }
        }
        else
        {
            message += "No evidence file selected.";
        }

        // Hiển thị tất cả dữ liệu trong MessageBox
        MessageBox.Show(message, "Submitted Data", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void ChooseFileButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        // Khởi tạo OpenFileDialog để chọn file
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
                                "Video Files (*.mp4; *.avi; *.mkv)|*.mp4;*.avi;*.mkv|" +
                                "All Files (*.*)|*.*";

        // Hiển thị dialog và kiểm tra nếu người dùng chọn file
        if (openFileDialog.ShowDialog() == true)
        {
            selectedFilePath = openFileDialog.FileName;

            // Cập nhật TextBox để hiển thị đường dẫn của file đã chọn
            SelectedFilePathTextBox.Text = selectedFilePath;
        }
    }
}
