# ResourceBookingCOJ

A system that will handle the day-to-day use and booking of resources within the organization.

## 🛠 Technologies Used

- ASP.NET Core MVC
- Entity Framework Core (with SQL Server or Sqlite)
- Bootstrap 5
- C#
- Razor Views

## 📦 Features

- Add, edit, delete, and view resources
- Book resources for time slots with validation and conflict checking
- Filter bookings by resource name and date
- View upcoming bookings on the dashboard
- Simple in-app management — no complex navigation
- Server-side and client-side validation
- Alert messages for success/error feedback


## 🚀 Getting Started

### Prerequisites

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- Visual Studio 2022 
- SQLite LocalDB (configured in `appsettings.json`)

### 🔧 Setup Instructions

1. **Clone the repository**:

```bash
git clone https://github.com/Bahumii/ResourceBookingCOJ
cd ResourceBookingCOJ

2. **Restore packages**:

dotnet restore

3. **Apply migrations and create database**:

dotnet ef database update

4. **Run the application**

dotnet run


