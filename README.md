
# Traffic Violation Management System

## Description
The "Traffic Violation Management System" allows citizens to report traffic violations, helping authorities process them quickly and efficiently. The system includes features such as submitting reports with images and videos, processing violations, and generating statistics and reports.

## Key Features
- **Report Traffic Violations:** Citizens can submit reports of traffic violations along with images and videos.
- **Manage Violation Information:** Violators can receive notifications and pay fines online.
- **Process Traffic Violations:** Traffic authorities review and process the reports.
- **Notifications and Status Tracking:** Both citizens and violators receive notifications about the status of their reports.
- **Reports and Statistics:** The system provides reports on the number of violations and their processing status.

## Technologies Used
- **Platform:** WPF (Windows Presentation Foundation)
- **Database:** SQL Server + EntityFrameworkCore
- **Architecture Pattern:** MVVM (Model-View-ViewModel)

## Project Structure
```
- Models: Database models.
- ViewModels: Logic and data handling for the views.
- Views: User interfaces (XAML).
- .gitignore: Files that are not needed in the git repository.
- App.xaml: Main application interface.
- App.xaml.cs: Application initialization logic.
- PRN212.csproj: .NET project file.
```

## Setup Instructions
1. **Install .NET SDK**: Ensure that the .NET SDK version 8.0 is installed.
2. **Install SQL Server**: You need SQL Server to store the data.
3. **Install NuGet Packages**: Open `PRN212.csproj` and run `dotnet restore` to fetch dependencies.
4. **Configure Database**: Create a SQL Server database and apply the table creation scripts from `script.sql`.

## How to Use
1. **Run the Application**: Open the project in Visual Studio or Visual Studio Code and run the application.
2. **Sign Up and Report Violations:** Citizens can register and submit reports for traffic violations.
3. **Manage Reports:** Traffic authorities process the reports and send notifications to violators.

## Reports
- The design and analysis report documentation is included in the project folder.
- Screenshots of the user interface and features are available in the documentation section.

## License
MIT License.
