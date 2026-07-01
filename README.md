# TaskForge

TaskForge is a lightweight, background-running Windows desktop productivity tracker. It monitors active application usage in real time, classifies activities into productivity categories, and provides visual insights, historical reports, and goal notifications to help users manage their time effectively.

Built using **C#**, **.NET 10**, and **Windows Forms**, the application follows a clean three-layer architecture and uses **Entity Framework Core** with **SQLite** for robust local persistence.

---

## Features

### 🕒 Real-Time Desktop Tracking
* **Foreground Window Monitoring**: Utilizes Windows P/Invoke calls to capture active window change events.
* **Continuous Background Monitoring**: Runs efficiently off the UI thread to ensure zero UI freezes or performance overhead.
* **System Tray Integration**: Operates quietly in the tray, supporting double-click main window toggling and exit actions.

### 📊 Visual Dashboards & Reports
* **Live Dashboard**: Displays real-time calculations of today's time spent per application and category via dynamic charts.
* **History Browser**: Review previous tracking sessions with date, application, and category filters.
* **Grouped Reports**: Formatted grid summaries of sessions categorized by application or productivity class with aggregate totals.
* **Column Chart Reports**: Visual daily/weekly distribution charts.

### 🎯 Productivity & Category Management
* **Category Configuration**: Create, modify, and delete custom categories (e.g., *Productive*, *Neutral*, *Distracting*).
* **Application Classification**: Map specific apps to custom categories to accurately compute productivity scores.
* **Goal Setting & Alerts**: Set daily time goals (in minutes) for target categories and receive desktop notifications when limits are reached.

### 💾 Backup, Export & Recovery
* **PDF Export**: Generate tabular and summary reports in PDF format using **QuestPDF**.
* **Excel Export**: Export tracked sessions directly to `.xlsx` files using **ClosedXML**.
* **Database Snapshot Backups**: Export or import database snapshots in JSON format to safeguard tracking data.

---

## Technical Architecture

TaskForge implements a strict **three-layer architecture** to separate concerns:

```
[ UI Layer (WinForms) ]
         │
         ▼
[ Service / Business Layer ]
         │
         ▼
[ Data Layer (EF Core + Repositories) ] ──▶ [ SQLite DB ]
```

1. **UI Layer (`Views/`)**: Contains only WinForms controls, chart rendering, and forms. No database or tracking logic runs here.
2. **Business/Service Layer (`Services/`)**: Manages the application logic, classifications, reporting, file export engines, and goal calculations.
3. **Data Layer (`Data/`, `Tracking/`)**: Manages database initialization, migrations, and SQLite queries through specialized Repository patterns.

---

## Getting Started

### Prerequisites
* Windows OS (required for foreground window tracking APIs)
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* IDE: Visual Studio 2022 or JetBrains Rider

### Installation & Run
1. Clone the repository:
   ```bash
   git clone https://github.com/chamesh2019/TaskForge.git
   cd TaskForge
   ```
2. Restore package dependencies:
   ```bash
   dotnet restore
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run --project TaskForge.csproj
   ```

Upon execution, the application will initialize a local SQLite database file named `TaskForge.db` in your output directory and start logging foreground window activity.

---

## Licenses & Third-Party Packages
* **Entity Framework Core (SQLite)** - ORM Data Access
* **QuestPDF** - PDF Generation (Community License)
* **ClosedXML** - Excel Spreadsheet Generation
