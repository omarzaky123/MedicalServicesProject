# MedicalServicesProject 🌡️

A **secure, role-based** web application for multi-branch medical service bookings with full-featured dashboards for different user roles.

![ASP.NET Core](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoft-sql-server)
![EF Core](https://img.shields.io/badge/Entity_Framework-8-512BD4?logo=dotnet)
![MVC](https://img.shields.io/badge/Architecture-MVC-5C2D91?logo=aspnet)

## 🔥 Key Features

- **Role-Based Access Control** (6 distinct roles)
- **Multi-Branch Management**
- **Real-Time Appointment System**
- **Interactive Dashboards**
- **Secure Authentication**
- **JSON Document Storage** (EF Core 8)
- **Hierarchical Data Support**

## 🛠 Tech Stack

### Frontend  
![HTML5](https://img.shields.io/badge/HTML5-E34F26?logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black)
![jQuery](https://img.shields.io/badge/jQuery-0769AD?logo=jquery&logoColor=white)
![AJAX](https://img.shields.io/badge/AJAX-Asynchronous-238636)

### Backend  
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET_MVC-8-512BD4?logo=aspnet)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-8.0-512BD4?logo=dotnet)

### Database  
![SQL Server](https://img.shields.io/badge/Microsoft_SQL_Server-2022-CC2927?logo=microsoft-sql-server)
![JSON Support](https://img.shields.io/badge/JSON-Native_Support-000000?logo=json)

### Architecture  
![MVC Pattern](https://img.shields.io/badge/Pattern-MVC-5C2D91?logo=aspnet)

---

## 🚀 New in This Version

- **SQL Server 2022** with enhanced performance and security
- **Entity Framework Core 8** with:
  - Improved JSON column support
  - Better hierarchical data handling
  - Enhanced LINQ translation
- **Modernized data access patterns**
- **Optimized query performance**

---

## 🌐 Live Demo

➡️ [http://medicalservices.runasp.net/](http://medicalservices.runasp.net/)  
*(Test credentials available upon request)*

---

## 🗄 Database Design

### Entity Relationship Diagram (ERD)
![ERD Diagram](images/ERD.png)  
*Visualizes all tables with relationships and constraints*

### ORM Mapping
![Mapping Diagram](images/Mappingpng.png)  
*Entity Framework Core 8 code-first implementation with SQL Server 2022*

---

## 🛠 Getting Started

```bash
# Clone repository
git clone https://github.com/your-username/MedicalServicesProject.git

# Restore packages
dotnet restore

# Apply migrations (requires SQL Server 2022)
dotnet ef database update

# Run application
dotnet run
