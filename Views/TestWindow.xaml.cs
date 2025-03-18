using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PRN212.Models;
using PRN212.Repositories;

namespace PRN212.Views
{
    public partial class TestWindow : Window
    {
        private int notificationId;

        public TestWindow(int notificationId)
        {
            InitializeComponent();
            this.notificationId = notificationId;
            LoadViolationDetails();
        }

        private void LoadViolationDetails()
        {
            // This is where you would normally fetch the data from your database
            // For demo purposes, we'll use mock data

            // Get notification details (in a real app, fetch from database)
            var violationDetails = GetMockViolationDetails(notificationId);

            // Set as DataContext for binding
            this.DataContext = violationDetails;

            // Set up video source if available
            if (!string.IsNullOrEmpty(violationDetails.VideoUrl))
            {
                VideoPlayer.Source = new Uri(violationDetails.VideoUrl);
            }
        }

        private ViolationDetailsViewModel GetMockViolationDetails(int notificationId)
        {
            // Sample data for demonstration
            return new ViolationDetailsViewModel
            {
                NotificationId = notificationId,
                DriverName = "Nguyen Van A",
                LicenseId = "DL-123456789",
                LicenseClass = "B2",
                ContactInfo = "+84 987 654 321",
                PlateNumber = "51F-123.45",
                VehicleMake = "Toyota",
                VehicleModel = "Camry",
                VehicleColor = "Silver",
                VehicleYear = "2020",
                ViolationType = "Speed Limit Violation",
                ViolationDateTime = "March 15, 2025 at 08:45 AM",
                ViolationLocation = "Nguyen Van Linh Boulevard, District 7, Ho Chi Minh City",
                FineAmount = "2,500,000 VND",
                DueDate = "April 15, 2025",
                ViolationDescription = "Vehicle was recorded traveling at 78 km/h in a 50 km/h zone. This is 28 km/h over the posted speed limit. The violation was captured by automated speed camera #S4572 and verified by traffic officer ID #TF-3842.",
                EvidenceImageUrl = "https://via.placeholder.com/400x300?text=Speed+Camera+Photo",
                VideoUrl = "https://www.youtube.com/watch?v=El7ERt3xH3Y", // Sample video URL
                MapImageUrl = "https://via.placeholder.com/400x200?text=Map+Location"
            };
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Pause();
        }
    }

    public class ViolationDetailsViewModel
    {
        public int NotificationId { get; set; }

        // Driver Information
        public string DriverName { get; set; }
        public string LicenseId { get; set; }
        public string LicenseClass { get; set; }
        public string ContactInfo { get; set; }

        // Vehicle Information
        public string PlateNumber { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleYear { get; set; }

        // Violation Details
        public string ViolationType { get; set; }
        public string ViolationDateTime { get; set; }
        public string ViolationLocation { get; set; }
        public string FineAmount { get; set; }
        public string DueDate { get; set; }
        public string ViolationDescription { get; set; }

        // Evidence
        public string EvidenceImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string MapImageUrl { get; set; }
    }
}