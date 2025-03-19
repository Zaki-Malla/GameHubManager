# GameHub Manager - Reservation & Inventory Management System

**GameHub Manager** is a comprehensive reservation and inventory management system designed specifically for **gaming hub owners**. It simplifies the management of gaming devices (such as PlayStation and PC), tracks reservations, monitors snack bar sales (e.g., biscuits and drinks), and provides real-time financial and inventory insights.

---

## Key Features

- **Device Reservations**: Owners can manage reservations for gaming devices and track reservation durations.
- **Real-Time Countdown**: Displays the remaining time for each active reservation.
- **Sales Tracking**: Tracks sales of snacks and drinks in real-time.
- **Snack Bar Inventory**: Manages inventory for items like biscuits and ensures stock levels are maintained.
- **Financial Reports**: Generates detailed daily income reports and sales statistics.
- **User Management**: Supports roles for owners and employees with different permissions.
- **User-Friendly Interface**: Modern design using the Phoenix theme for an intuitive experience.

---

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: HTML, CSS, JavaScript, Phoenix Theme, Bootstrap
- **Database**: SQLite
- **Other Tools**: Entity Framework Core, Font Awesome

---

## License

This project is licensed under a [Custom License](LICENSE.txt).  
Unauthorized use, distribution, or sale of this software is strictly prohibited and may result in legal action.

---

## How to Run the Project

### Prerequisites
- .NET 8.0 SDK or later
- SQLite (or any other database supported by EF Core)

### Steps to Run


# 1. **Clone the Repository**
git clone https://github.com/Zaki-Malla/GameHubManager.git
cd GameHubManager

# 2. **Install Required Packages**
dotnet restore

# 3. **Apply Migrations and Create the Database**
dotnet ef database update

# 4. **Run the Project**
dotnet run

# 5. **Open your browser and navigate to:**
# https://localhost:7093
