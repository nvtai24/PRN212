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
        public MyReportDetailPage()
        {
            InitializeComponent();
            LoadDataDetail();
        }

        void LoadDataDetail()
        {
            var report = Application.Current.Properties["report"] as Report;
            if (report != null)
            {
                this.ReportIdTextBox.Text = report.ReportId.ToString();
                this.StatusTextBox.Text = report.Status;
                this.LocationTextBox.Text = report.Location;
                this.ViolationTypeTextBox.Text = report.ViolationType;
                this.LicensePlateTextBox.Text = report.PlateNumber;
                this.DescriptionTextBox.Text = report.Description;

                // Đường dẫn tới thư mục Assets
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
                        VideoDisplay.MediaEnded += (s, e) => {
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
                PlayButton.Click += (s, e) => {
                    if (VideoDisplay.Source != null)
                        VideoDisplay.Play();
                };

                PauseButton.Click += (s, e) => {
                    if (VideoDisplay.Source != null)
                        VideoDisplay.Pause();
                };
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
