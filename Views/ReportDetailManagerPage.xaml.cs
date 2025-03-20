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
using System.Windows.Controls.Primitives;
using System.Security.Cryptography;

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

            if (!report.Status.Equals("Approved"))
            {
                this.PenaltyBtn.Visibility = Visibility.Collapsed;
            }

            ReportManagerDAO reportDao = new ReportManagerDAO();
            string plateCheck = this.LicensePlateTextBox.Text;

            if (reportDao.CheckPlate(plateCheck))
            {
                this.ApproveBtn.Visibility = Visibility.Collapsed;

                // Hiển thị TextBlock "Plate number is invalid"
                this.InvalidPlateTextBlock.Visibility = Visibility.Visible;

                // Thay đổi màu khung của TextBox thành màu đỏ
                this.LicensePlateTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                // Ẩn TextBlock khi Plate hợp lệ
                this.InvalidPlateTextBlock.Visibility = Visibility.Collapsed;

                // Khôi phục lại màu khung của TextBox
                this.LicensePlateTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
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

                if (report.ProcessedByNavigation != null)
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
            ViolationDAO vdao = new ViolationDAO();
            var violation = vdao.GetViolationByReport(report.ReportId);
            if (violation != null)
            {
                this.PenaltyBtn.Content = "View penalty sent";
                this.PaidStatusTextBox.Visibility = Visibility.Visible;
                this.PaidStatusText.Visibility = Visibility.Visible;
                this.ConfirmPenalty.Visibility = Visibility.Collapsed;
                this.FineAmountTextBox.Text = violation.FineAmount.ToString();
                this.ConfirmPaid.Visibility = Visibility.Visible;
                if (violation.PaidStatus == true)
                {
                    this.PaidStatusTextBox.Text = "Paid";
                }
                else
                {
                    this.PaidStatusTextBox.Text = "Unpaid";
                }
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
             ReportManagerDAO reportDao = new ReportManagerDAO();
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Are you sure you want to approve this report?", "Confirm Approval", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Tiến hành approve nếu người dùng chọn Yes
                var report = Application.Current.Properties["reportManager"] as Report;
                var user = Application.Current.Properties["User"] as User;

                reportDao.ApproveReport(report.ReportId, user.UserId);

                this.StatusTextBox.Text = "Approved";
                this.ProcessbyTextBox.Text = user.FullName;

                MessageBox.Show("Approve Successfully!");
                this.ApproveBtn.Visibility = Visibility.Collapsed;
                this.RejectBtn.Visibility = Visibility.Collapsed;
                this.PenaltyBtn.Visibility = Visibility.Visible;
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

        private void PenaltyBtn_Click(object sender, RoutedEventArgs e)
        {
            // Fill the popup with existing data
            var report = Application.Current.Properties["reportManager"] as Report;

            if (report != null)
            {
                PenaltyPlateNumberTextBox.Text = report.PlateNumber;
            }

            // Show the popup
            PenaltyPopup.IsOpen = true;
        }

        private void CancelPenalty_Click(object sender, RoutedEventArgs e)
        {
            PenaltyPopup.IsOpen = false;
        }

        // Handle the Send button in the popup
        private void SendPenalty_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FineAmountTextBox.Text) || !decimal.TryParse(FineAmountTextBox.Text, out decimal fineAmount))
            {
                MessageBox.Show("Please enter a valid fine amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ReportManagerDAO reportDao = new ReportManagerDAO();

            // Get paid status from combo box
            string paidStatus = PaidStatusTextBox.Text;
            string plate = PenaltyPlateNumberTextBox.Text;
            string amount = FineAmountTextBox.Text;
            string rid = ReportIdTextBox.Text;
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            User violator = reportDao.GetViolator(plate);

            Violation v = new Violation();
            v.ReportId = Int32.Parse(rid);
            v.PlateNumber = plate;
            v.ViolatorId = violator.UserId;
            v.FineAmount = fineAmount;
            v.FineDate = DateTime.Now;
            v.PaidStatus = false;

            Notification notice = new Notification();
            notice.UserId = violator.UserId;
            notice.Message = "You have received a traffic violation penalty notice for your vehicle " + plate + " due to [" + this.ViolationTypeTextBox.Text + "]";
            notice.PlateNumber = plate;
            notice.SentDate = DateTime.Now;

            ViolationDAO vdao = new ViolationDAO();
            vdao.SendViolation(v, notice);

            //MessageBox.Show(rid + ", " +plate +", " + amount +", " + violator.FullName + ", " + currentDate.ToString());

            // For now, just show a success message
            MessageBox.Show("Penalty has been sent successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            this.PenaltyBtn.Content = "View penalty sent";
            this.PaidStatusTextBox.Visibility = Visibility.Visible;
            this.PaidStatusText.Visibility = Visibility.Visible;
            this.ConfirmPenalty.Visibility = Visibility.Collapsed;
            this.ConfirmPaid.Visibility = Visibility.Visible;
            if (v.PaidStatus == true)
            {
                this.PaidStatusTextBox.Text = "Paid";
            }
            else
            {
                this.PaidStatusTextBox.Text = "Unpaid";
            }
                // Close the popup
                PenaltyPopup.IsOpen = false;
        }

        private void ConfirmPaid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Kiểm tra nếu người dùng click bên ngoài pop-up để đóng pop-up
            if (e.OriginalSource is Popup)
            {
                PenaltyPopup.IsOpen = false;
            }
        }
    }
}
