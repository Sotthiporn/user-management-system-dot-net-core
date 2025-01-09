# User Management System with JWT Authentication

## Overview
A **Web API** built with **C#** and **.NET Core**, featuring **JWT authentication** for user management, role-based authorization, and refresh token functionality.

---

## Technologies Used
- **.NET Core v6**
- **Entity Framework Core**
- **SQLite**
- **JWT** Authentication
- **BCrypt** (Generate Hash)
- **Swagger** (API documentation)

---

## Features
- **Authentication**
   - Login  
   - Register  
   - Refresh Token
- **User Management**
   - Get all user (Only admin role)
   - Get specific user
   - Update user profile (Email & Username) 
   - Delete specific user (Only admin role)


## Setup

1. **Install .NET CLI and EF Core for Migration**:
   - Download and install the .NET CLI from [here](https://dotnet.microsoft.com/download).
   - Install EF Core CLI tools for migration (Ingore this if dont want to add more migration):
   ```bash
   dotnet tool install --global dotnet-ef
   ```
 
2. **Clone the Repository**:
```bash
git clone https://github.com/Sotthiporn/user-management-system-dot-net-core.git
cd user-management-system-dot-net-core
```

3. **Configure the Project**:
   - Rename `appsettings.Example.json` to `appsettings.Development.json`
   - Update `appsettings.Development.json` with your JWT configuration and sqlite path (You can use everything like example).

4. **Run the Application**:
```bash
dotnet run
```

## Access API
- The app runs at http://localhost:5000.
- Swagger UI for testing: http://localhost:5000/swagger.

---

## Default Admin
- "username": "admin",
- "password": "Admin123!"

## Migration
- Folder: Database/Migrations

---

## Seeder
- Folder: Database/Seeders

---

## SQLite
- Folder: Database/SQLite

**Author**: [Seum Sotthiporn]

---