# GameHub Manager  
**Reservation & Inventory Management System**

**GameHub Manager** is a comprehensive reservation and inventory management system built specifically for gaming hub owners. It streamlines device reservations (e.g., PlayStation, PC), tracks reservation durations, monitors snack bar sales (biscuits, drinks), and delivers real-time financial and inventory insights.

---

## 🔑 Key Features

- **Device Reservations**  
  Easily create, view, and manage reservations for gaming devices.  
- **Real-Time Countdown**  
  Displays remaining time for each active reservation at a glance.  
- **Sales Tracking**  
  Records snack bar sales (biscuits, drinks) in real time.  
- **Snack Bar Inventory**  
  Manages stock levels for items like biscuits and alerts you before they run out.  
- **Comprehensive Reports**  
  Offers in-depth daily and monthly statistics covering all aspects of the system—from reservations and sales to inventory and user activities—highlighting the strength of interlinked data relationships within the database.  
- **User Management**  
  Role-based access for Owners and Employees, with customizable permissions.  
- **Modern UI**  
  Sleek design using the Phoenix Theme and Bootstrap for an intuitive experience.  
- **Multi-Language Support**  
  Switch seamlessly between **English** and **Arabic** via RESX resource files.  
- **Localization**  
  Automatically adapts the interface and text based on the selected language.

---

## 💾 Automatic Database Backup

On every application startup, the system automatically creates a timestamped backup copy of the database file into a dedicated folder named `AppData_backup`. This ensures your data is preserved and can be restored if needed.

---

## 🛠️ Technologies Used

- **Backend:** ASP.NET Core MVC  
- **Frontend:** HTML, CSS, JavaScript, Phoenix Theme, Bootstrap  
- **Database:** SQLite with Entity Framework Core  
- **Icons:** Font Awesome

---

## ⚙️ Prerequisites

- .NET 9.0 SDK or later  
- SQLite (bundled via EF Core)

---

## 🚀 How to Run the Project from Source Code

```bash
# 1. Clone the Repository
git clone https://github.com/Zaki-Malla/GameHubManager.git
cd GameHubManager

# 2. Restore Required Packages
dotnet restore

# 3. Apply Migrations and Create the Database
dotnet ef database update

# 4. Run the Project
dotnet run

# 5. Open your browser and navigate to:
https://localhost:5000
```

---

## 📦 Download Ready-to-Use Release

If you don't want to build the project from source, you can download the latest **precompiled release** directly from the [Releases Section](https://github.com/Zaki-Malla/GameHubManager/releases).  
These releases include everything you need to run the system immediately.

---

## 📄 License

This project is licensed under a **[Custom License](LICENSE.txt)**.  
Unauthorized use, distribution, or sale is strictly prohibited and may result in legal action.

