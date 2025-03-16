using System.Linq;
using System.Windows.Controls;
using PRN212.Repositories;

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
            var reports = reportDAO.GetReportsById(1);

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
    }
}
