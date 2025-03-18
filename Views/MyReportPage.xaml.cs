using System.Linq;
using System.Windows.Controls;
using PRN212.Repositories;
using PRN212.Models;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Cryptography;

namespace PRN212.Views
{
    public partial class MyReportPage : Page
    {
        public MyReportPage()
        {
            InitializeComponent();
            LoadDataGridReports();
        }

        // Load data with optional filtering
        void LoadDataGridReports(string status = "All Status", DateTime? startDate = null, DateTime? endDate = null)
        {
            ReportDAO reportDAO = new ReportDAO();

            var user = Application.Current.Properties["User"] as Models.User;

            var reports = reportDAO.GetReportsById(user.UserId);

            // Apply status filter only if a valid status is selected
            if (status != "All Status")
            {
                reports = reports.Where(r => r.Status == status).ToList();
            }

            // Apply date range filter (if provided)
            if (startDate.HasValue && endDate.HasValue)
            {
                reports = reports.Where(r => r.ReportDate.HasValue && r.ReportDate.Value.Date >= startDate.Value.Date
                                              && r.ReportDate.Value.Date <= endDate.Value.Date).ToList();
            }

            // Sort by ReportDate (descending)
            reports = reports.OrderByDescending(r => r.ReportDate).ToList();

            // Bind filtered and sorted data to the DataGrid
            this.ReportsDataGrid.ItemsSource = reports;
        }

        // Handle the Apply Filters button click
        private void ApplyFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string selectedStatus = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();
            DateTime? selectedStartDate = StartDatePicker.SelectedDate;
            DateTime? selectedEndDate = EndDatePicker.SelectedDate;

            // Call LoadDataGridReports with selected status, start date, and end date
            LoadDataGridReports(selectedStatus, selectedStartDate, selectedEndDate);
        }

        private void ReportDetailPage_Click(object sender, RoutedEventArgs e)
        {
            var report = this.ReportsDataGrid.SelectedItem as Report;
            Application.Current.Properties["report"] = report;
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainFrame.Content = new MyReportDetailPage();

        }
    }
}
