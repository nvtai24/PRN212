using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MyReportDetailPage.xaml
    /// </summary>
    public partial class MyReportDetailPage : Page
    {
        private string selectedImagePath;
        private string selectedVideoPath;
        public MyReportDetailPage()
        {
            InitializeComponent();
            LoadDataDetail();
            LoadComboBox();

            var report = Application.Current.Properties["report"] as Report;

            if(report != null)
            {
                if (!report.Status.Equals("Pending"))
                {
                    this.UpdateBtn.Visibility = Visibility.Collapsed;
                    this.CancelBtn.Visibility = Visibility.Collapsed;
                }
            }
        }

        void LoadDataDetail()
        {
            var report = Application.Current.Properties["report"] as Report;
            if (report != null)
            {
                this.ReportIdTextBox.Text = report.ReportId.ToString();
                if(report.ProcessedByNavigation != null)
                {
                    this.ProcessByTextBox.Text = report.ProcessedByNavigation.FullName;
                }
                this.StatusTextBox.Text = report.Status;
                this.LocationTextBox.Text = report.Location;
                this.ViolationTypeTextBox.Text = report.ViolationType;
                //this.LicensePlateComboBox.SelectedValue = report.PlateNumber;
                this.DescriptionTextBox.Text = report.Description;

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

        private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Select an Image"
            };

            if (fileDialog.ShowDialog() == true)
            {
                selectedImagePath = fileDialog.FileName;
                ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(selectedImagePath);

                // Hiển thị preview ảnh
                ImageDisplay.Visibility = Visibility.Visible;
                ImageDisplay.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private void ChooseVideoButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.mkv",
                Title = "Select a Video"
            };

            if (fileDialog.ShowDialog() == true)
            {
                selectedVideoPath = fileDialog.FileName;
                VideoFileNameTextBlock.Text = System.IO.Path.GetFileName(selectedVideoPath);

                // Hiển thị preview video
                VideoDisplay.Visibility = Visibility.Visible;
                VideoDisplay.Source = new Uri(selectedVideoPath);

                // Phát video khi đã chọn
                VideoDisplay.Play();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            var reportSession = Application.Current.Properties["report"] as Report;

            // Kiểm tra nếu có ảnh được chọn hoặc đã hiển thị ảnh
            if (ImageDisplay.Visibility == Visibility.Visible && !string.IsNullOrEmpty(selectedImagePath))
            {
                string imageFileName = System.IO.Path.GetFileName(selectedImagePath);
                string projectFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");
                string imageFolder = System.IO.Path.Combine(projectFolder, "Images");

                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }

                string newImagePath = System.IO.Path.Combine(imageFolder, imageFileName);

                if (reportSession.ImageUrl == null || !reportSession.ImageUrl.Equals(newImagePath, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        // Giải phóng tệp ảnh khỏi WPF Image control
                        ImageDisplay.Source = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        // Kiểm tra và xóa file nếu đã tồn tại
                        if (File.Exists(newImagePath))
                        {
                            File.SetAttributes(newImagePath, FileAttributes.Normal);
                            File.Delete(newImagePath);
                        }

                        File.Copy(selectedImagePath, newImagePath, true);
                        report.ImageUrl = System.IO.Path.Combine("Images", imageFileName);

                        // Load lại ảnh vào ImageDisplay
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.UriSource = new Uri(newImagePath, UriKind.Absolute);
                        bitmap.EndInit();
                        ImageDisplay.Source = bitmap;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật ảnh: " + ex.Message);
                    }
                }
            }


            else
            {
                report.ImageUrl = null;
            }

            // Kiểm tra nếu có video được chọn hoặc đã hiển thị video
            if (VideoDisplay.Visibility == Visibility.Visible && !string.IsNullOrEmpty(selectedVideoPath))
            {
                string videoFileName = System.IO.Path.GetFileName(selectedVideoPath);
                string projectFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets");
                string videoFolder = System.IO.Path.Combine(projectFolder, "Videos");

                if (!Directory.Exists(videoFolder))
                {
                    Directory.CreateDirectory(videoFolder);
                }

                string newVideoPath = System.IO.Path.Combine(videoFolder, videoFileName);
                File.Copy(selectedVideoPath, newVideoPath, true);

                // Gán đường dẫn video vào đối tượng report
                report.VideoUrl = System.IO.Path.Combine("Videos", videoFileName);
            }
            else
            {
                report.VideoUrl = null;
            }

            if (report.ImageUrl == null)
            {
                report.ImageUrl = reportSession.ImageUrl;
            }

            if (report.VideoUrl == null)
            {
                report.VideoUrl = reportSession.VideoUrl;
            }

            report.ReportId = reportSession.ReportId;
            report.ReporterId = reportSession.ReporterId;
            report.Location = this.LocationTextBox.Text;
            report.PlateNumber = this.LicensePlateComboBox.SelectedValue.ToString();
            report.ViolationType = this.ViolationTypeTextBox.Text;
            report.Description = this.DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(report.Location))
            {
                MessageBox.Show("Please enter location.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(report.ViolationType))
            {
                MessageBox.Show("Please enter violation type.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(report.Description))
            {
                MessageBox.Show("Please enter description.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ReportDAO reportDao = new ReportDAO();
            reportDao.UpdateReport(report);

            MessageBox.Show("Update successfully!");
            //MessageBox.Show(report.VideoUrl + "\n" + report.ImageUrl +"\n" + report.ReporterId + "\n" + report.ReportId
            //   +"\n" + report.Location +"\n" + report.PlateNumber +"\n" + report.ViolationType +"\n" + report.Description);
        }

        void LoadComboBox()
        {
            ReportDAO dao = new ReportDAO();
            var report = Application.Current.Properties["report"] as Report;
            var user = Application.Current.Properties["User"] as Models.User;
            // Load danh sách biển số vào ComboBox
            this.LicensePlateComboBox.ItemsSource = dao.GetPlates(user.UserId);
            this.LicensePlateComboBox.DisplayMemberPath = "PlateNumber";
            this.LicensePlateComboBox.SelectedValuePath = "PlateNumber";

            // Đảm bảo ComboBox có ít nhất một giá trị để chọn
            if (this.LicensePlateComboBox.ItemsSource != null)
            {
                // Thiết lập giá trị mặc định của ComboBox
                this.LicensePlateComboBox.SelectedValue = report.PlateNumber;

                // Nếu không tìm thấy giá trị PlateNumber trong ItemsSource, bạn có thể đặt SelectedIndex = 0
                // Nhưng ở đây ta sẽ chọn dựa trên PlateNumber
                if (this.LicensePlateComboBox.SelectedValue == null)
                {
                    this.LicensePlateComboBox.SelectedIndex = 0;
                }
            }
        }

        private void MyReportDetailPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Xóa giá trị trong Application.Current.Properties khi rời khỏi trang
            Application.Current.Properties.Remove("report");
        }

        private void BackToReportList_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainFrame.Content = new MyReportPage();
        }
    }
}
