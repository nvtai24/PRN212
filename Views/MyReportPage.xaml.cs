using System.Linq;
using System.Windows.Controls;
using PRN212.Repositories;
using PRN212.Models;
using System.Windows;
using System;

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
        void LoadDataGridReports(string status = "All Status", DateTime? startDate = null, DateTime? endDate = null, string searchQuery = "")
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

            // Apply search filter (if provided)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                reports = reports.Where(r => r.ViolationType.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sort by ReportDate (descending)
            reports = reports.OrderByDescending(r => r.ReportDate).ToList();

            // Bind filtered and sorted data to the DataGrid
            this.ReportsDataGrid.ItemsSource = reports;
        }

        // Handle the Apply Filters button click
        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedStatus = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();
            DateTime? selectedStartDate = StartDatePicker.SelectedDate;
            DateTime? selectedEndDate = EndDatePicker.SelectedDate;

            // Get the search query from SearchBox
            string searchQuery = SearchBox.Text.Trim();

            // Call LoadDataGridReports with selected filters and search query
            LoadDataGridReports(selectedStatus, selectedStartDate, selectedEndDate, searchQuery);
        }

        // Handle the SearchBox TextChanged event to filter reports dynamically
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = SearchBox.Text.Trim();
            string selectedStatus = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();
            DateTime? selectedStartDate = StartDatePicker.SelectedDate;
            DateTime? selectedEndDate = EndDatePicker.SelectedDate;

            // Call LoadDataGridReports with the updated search query
            LoadDataGridReports(selectedStatus, selectedStartDate, selectedEndDate, searchQuery);
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
