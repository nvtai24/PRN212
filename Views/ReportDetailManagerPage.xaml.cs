using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRN212.Models;
using Microsoft.Win32;
using System.IO;
using PRN212.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PRN212.Views
{
    /// <summary>
    /// Interaction logic for ReportDetailManagerPage.xaml
    /// </summary>
    public partial class ReportDetailManagerPage : Page
    {
        public ReportDetailManagerPage()
        {
            InitializeComponent();
            LoadDataDetail();

            var report = Application.Current.Properties["reportManager"] as Report;
            if(!report.Status.Equals("Pending"))
            {
                this.ApproveBtn.Visibility = Visibility.Collapsed;
                this.RejectBtn.Visibility = Visibility.Collapsed;
            }
        }

        void LoadDataDetail()
        {
            var report = Application.Current.Properties["reportManager"] as Report;
            if (report != null)
            {
                this.ReportIdTextBox.Text = report.ReportId.ToString();
                this.StatusTextBox.Text = report.Status;
                this.LocationTextBox.Text = report.Location;
                this.ViolationTypeTextBox.Text = report.ViolationType;
                this.LicensePlateTextBox.Text = report.PlateNumber;
                this.DescriptionTextBox.Text = report.Description;

                if(report.ProcessedByNavigation != null)
                {
                    this.ProcessbyTextBox.Text = report.ProcessedByNavigation.FullName;
                }

                //Đường dẫn tới thư mục Assets
                string projectFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");

                // Hiển thị ảnh nếu có
                if (!string.IsNullOrEmpty(report.ImageUrl))
                {
                    string imagePath = System.IO.Path.Combine(projectFolder, report.ImageUrl);
                    if (File.Exists(imagePath))
                    {
                        Uri imageUri = new Uri(imagePath, UriKind.Absolute);
                        ImageDisplay.Source = new BitmapImage(imageUri);
                        ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(report.ImageUrl);
                    }
                    else
                    {
                        ImageDisplay.Source = null;
                        ImageFileNameTextBlock.Text = "No image available";
                    }
                }
                else
                {
                    ImageDisplay.Source = null;
                    ImageFileNameTextBlock.Text = "No image available";
                }

                // Hiển thị video nếu có
                if (!string.IsNullOrEmpty(report.VideoUrl))
                {
                    string videoPath = System.IO.Path.Combine(projectFolder, report.VideoUrl);
                    if (File.Exists(videoPath))
                    {
                        // Đặt visibility thành Visible (giống như trong code của bạn)
                        VideoDisplay.Visibility = Visibility.Visible;

                        // Đặt Uri cho video
                        VideoDisplay.Source = new Uri(videoPath, UriKind.Absolute);

                        // Phát video ngay sau khi load
                        VideoDisplay.Play();

                        // Thiết lập để video tự động phát lại
                        VideoDisplay.MediaEnded += (s, e) =>
                        {
                            VideoDisplay.Position = TimeSpan.Zero; // Quay lại đầu video
                            VideoDisplay.Play(); // Phát lại video
                        };

                        VideoFileNameTextBlock.Text = System.IO.Path.GetFileName(report.VideoUrl);
                    }
                    else
                    {
                        VideoDisplay.Source = null;
                        VideoFileNameTextBlock.Text = "No video available";
                    }
                }
                else
                {
                    VideoDisplay.Source = null;
                    VideoFileNameTextBlock.Text = "No video available";
                }

                // Thêm code xử lý nút Play và Pause
                PlayButton.Click += (s, e) =>
                {
                    if (VideoDisplay.Source != null)
                        VideoDisplay.Play();
                };

                PauseButton.Click += (s, e) =>
                {
                    if (VideoDisplay.Source != null)
                        VideoDisplay.Pause();
                };
            }
        }

        private void MyReportDetailPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Xóa giá trị trong Application.Current.Properties khi rời khỏi trang
            Application.Current.Properties.Remove("reportManager");
        }

        private void BackToReportList_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainFrame.Content = new ManageReportPage();
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Are you sure you want to approve this report?", "Confirm Approval", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Tiến hành approve nếu người dùng chọn Yes
                ReportManagerDAO reportDao = new ReportManagerDAO();
                var report = Application.Current.Properties["reportManager"] as Report;
                var user = Application.Current.Properties["User"] as User;

                reportDao.ApproveReport(report.ReportId, user.UserId);

                this.StatusTextBox.Text = "Approved";
                this.ProcessbyTextBox.Text = user.FullName;

                MessageBox.Show("Approve Successfully!");
                this.ApproveBtn.Visibility = Visibility.Collapsed;
                this.RejectBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Nếu người dùng chọn No, không làm gì
                MessageBox.Show("Approval canceled.");
            }
        }


        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Are you sure you want to reject this report?", "Confirm Rejection", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Tiến hành reject nếu người dùng chọn Yes
                ReportManagerDAO reportDao = new ReportManagerDAO();
                var report = Application.Current.Properties["reportManager"] as Report;

                reportDao.RejectReport(report.ReportId);

                this.StatusTextBox.Text = "Rejected";

                MessageBox.Show("Rejected Successfully!");
                this.ApproveBtn.Visibility = Visibility.Collapsed;
                this.RejectBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Nếu người dùng chọn No, không làm gì
                MessageBox.Show("Rejection canceled.");
            }
        }

    }
}
