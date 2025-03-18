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
            //MessageBox.Show(report.ReportId.ToString());
            if (report != null)
            {
                this.ReportIdTextBox.Text = report.ReportId.ToString();
                this.StatusTextBox.Text = report.Status;
                this.LocationTextBox.Text = report.Location;
                this.ViolationTypeTextBox.Text = report.ViolationType;
                this.LicensePlateTextBox.Text = report.PlateNumber;
                this.DescriptionTextBox.Text = report.Description;

                if (!string.IsNullOrEmpty(report.ImageUrl) && System.IO.File.Exists(report.ImageUrl))
                {
                    ImageDisplay.Source = new BitmapImage(new Uri("../" + report.ImageUrl));
                    ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(report.ImageUrl);  // Hiển thị tên ảnh
                }
                else
                {
                    ImageFileNameTextBlock.Text = "No image available";
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
